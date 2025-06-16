
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.Models;
using System.Linq.Expressions;
using Shoghlana.EF.Repositories;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Enums;

namespace Shoghlana.API.Services.Implementations;

public class ProposalService : GenericService<Proposal>, IProposalService
{
    private readonly IMapper _mapper;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };

    private long maxAllowedImageSize = 1_048_576;  // 1 MB 

    public ProposalService(IUnitOfWork unitOfWork, IGenericRepository<Proposal> repository, IMapper mapper)
           : base(unitOfWork, repository)
    {
        _mapper = mapper;
    }


    public async Task<ActionResult<GeneralResponse>> GetAllAsync()
    {
        var proposals = _unitOfWork.proposalRepository.FindAll(new string[] { "Images" }).ToList();

        var getProposalsDTOs = new List<GetProposalDTO>(proposals.Count);

        foreach (var proposal in proposals)
        {
            var getProposalsDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            getProposalsDTOs.Add(getProposalsDTO);
        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = getProposalsDTOs,
        };
    }


    public ActionResult<GeneralResponse> GetById(int id)
    {
        var proposal = _unitOfWork.proposalRepository.GetById(id);

        if (proposal == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Proposal found with this ID !"
            };
        }
        var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = getProposalDTO,
        };
    }


    public async Task<ActionResult<GeneralResponse>> GetByJobIdAsync(int id)
    {
        var job = await _unitOfWork.proposalRepository.GetByIdAsync(id);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = "There is no Job found with this ID !"
            };
        }

        var proposals = _unitOfWork.proposalRepository.FindAll(includes: null, p => p.JobId == id).ToList();

        if (proposals.Count == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = "There are no proposals yet to this job ."
            };
        }

        var getProposalDTOs = new List<GetProposalDTO>(proposals.Count);

        foreach (var proposal in proposals)
        {
            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            getProposalDTOs.Add(getProposalDTO);

        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = getProposalDTOs,
        };
    }


    public async Task<ActionResult<GeneralResponse>> GetByFreelancerIdAsync(int id)
    {
        var freelancer = await _unitOfWork.freelancerRepository.GetByIdAsync(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"There are no freelancer found with this ID {id} ."
            };
        }

        var proposals = _unitOfWork.proposalRepository.FindAll(includes: null, p => p.FreelancerId == id).ToList();

        if (proposals.Count == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = "There are no proposals yet to this freelancer ."
            };
        }

        var getProposalDTOs = new List<GetProposalDTO>();

        foreach (var proposal in proposals)
        {
            var jobDTO = _unitOfWork.jobRepository.Find(j => j.Id == proposal.JobId, new string[] { "Client" });

            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            getProposalDTO.JobTitle = jobDTO.Title;
            getProposalDTO.ClientName = jobDTO?.Client.Name;

            getProposalDTOs.Add(getProposalDTO);
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = getProposalDTOs,
        };
    }


    public async Task<ActionResult<GeneralResponse>> AddAsync([FromForm] AddProposalDTO addProposalDTO)
    {
        var job = await _unitOfWork.jobRepository.GetByIdAsync(addProposalDTO.JobId);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No job found with this ID: {addProposalDTO.JobId}!"
            };
        }

        var freelancer = await _unitOfWork.freelancerRepository.GetByIdAsync(addProposalDTO.FreelancerId);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No freelancer found with this ID: {addProposalDTO.FreelancerId}!"
            };
        }

        if (addProposalDTO.Images is not null && addProposalDTO.Images.Count > 0)
        {
            var proposalImages = new List<ProposalImages>();

            foreach (var addProposalImageDTO in addProposalDTO.Images)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(addProposalImageDTO.Image.FileName).ToLower()))
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The allowed Image Extensions => {jpg , png}",
                    };
                }
                if (addProposalImageDTO.Image.Length > maxAllowedImageSize)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The max Allowed Personal Image Size => 1 MB ",
                    };
                }

                using var dataStream = new MemoryStream();

                await addProposalImageDTO.Image.CopyToAsync(dataStream);

                var propsalImage = new ProposalImages()
                {
                    //Id = addProposalImageDTO.Id,
                    Image = dataStream.ToArray()
                    //ProposalId = addProposalImageDTO.ProposalId,
                };
                proposalImages.Add(propsalImage);
            }

            var propsal = new Proposal()
            {
                Images = proposalImages,

                Duration = addProposalDTO.Duration,

                Description = addProposalDTO.Description,

                ReposLinks = addProposalDTO.ReposLinks,

                FreelancerId = addProposalDTO.FreelancerId,

                JobId = addProposalDTO.JobId,

            };
            //proposal = mapper.Map<AddProposalDTO, Proposal>(addProposalDTO);

            var addProposal = await _unitOfWork.proposalRepository.AddAsync(propsal);

            _unitOfWork.Save();

            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(addProposal);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 201,
                Data = getProposalDTO,
                Message = "Proposal Added Successfully"
            };
        }
        else
        {
            var proposal = new Proposal()
            {
                Description = addProposalDTO.Description,

                Duration = addProposalDTO.Duration,

                Price = addProposalDTO.Price,

                FreelancerId = addProposalDTO.FreelancerId,

                JobId = addProposalDTO.JobId,

                ReposLinks = addProposalDTO.ReposLinks,

                Images = null
            };
            var addProposal = await _unitOfWork.proposalRepository.AddAsync(proposal);

            _unitOfWork.Save();

            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 201,
                Data = getProposalDTO,
                Message = "Proposal Added Successfully"
            };
        }
    }


    // TODO : Try To use Async in Find to reduce waiting time
    public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddProposalDTO addProposalDTO)
    {
        var proposal = _unitOfWork.proposalRepository.Find(p => p.Id == id, new string[] { "Images" });

        if (proposal is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"There is no Proposal found with this ID : {id}!"
            };
        }

        var job = await _unitOfWork.jobRepository.GetByIdAsync(addProposalDTO.JobId);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No job found with this ID: {addProposalDTO.JobId}!"
            };
        }

        var freelancer = await _unitOfWork.freelancerRepository.GetByIdAsync(addProposalDTO.FreelancerId);

        if (freelancer == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No freelancer found with this ID: {addProposalDTO.FreelancerId}!"
            };
        }

        if (proposal.Images is not null || proposal?.Images?.Count > 0)
        {
            foreach (var image in proposal.Images)
            {
                image.ProposalId = proposal.Id;

                _unitOfWork.proposalImageRepository.Delete(image);
            }
        }
        if (addProposalDTO.Images is not null && addProposalDTO.Images.Count > 0)
        {
            var proposalImages = new List<ProposalImages>();

            foreach (var addProposalImageDTO in addProposalDTO.Images)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(addProposalImageDTO.Image.FileName).ToLower()))
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The allowed Image Extensions => {jpg , png}",
                    };
                }
                if (addProposalImageDTO.Image.Length > maxAllowedImageSize)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The max Allowed Personal Image Size => 1 MB ",
                    };
                }

                using var dataStream = new MemoryStream();

                await addProposalImageDTO.Image.CopyToAsync(dataStream);

                var propsalImage = new ProposalImages()
                {
                    Image = dataStream.ToArray()
                };

                proposalImages.Add(propsalImage);
            }

            proposal.Images = proposalImages;

            proposal.Duration = addProposalDTO.Duration;

            proposal.Description = addProposalDTO.Description;

            proposal.ReposLinks = addProposalDTO.ReposLinks;

            proposal.FreelancerId = addProposalDTO.FreelancerId;

            proposal.Price = addProposalDTO.Price;

            proposal.JobId = addProposalDTO.JobId;

            _unitOfWork.Save();

            var editedProposal = _unitOfWork.proposalRepository.Find(p => p.Id == id, new string[] { "Images" });

            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(editedProposal);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 201,
                Data = getProposalDTO,
                Message = "Proposal Added Successfully"
            };
        }
        else
        {
            proposal.Description = addProposalDTO.Description;

            proposal.Duration = addProposalDTO.Duration;

            proposal.Price = addProposalDTO.Price;

            proposal.FreelancerId = addProposalDTO.FreelancerId;

            proposal.JobId = addProposalDTO.JobId;

            proposal.ReposLinks = addProposalDTO.ReposLinks;

            proposal.Images = null;

            _unitOfWork.Save();

            var getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 201,
                Data = getProposalDTO,
                Message = "Proposal Added Successfully"
            };
        }
    }


    public ActionResult<GeneralResponse> Delete(int id)
    {
        var proposal = _unitOfWork.proposalRepository.Find(criteria: p => p.Id == id, new string[] { "Images" });

        if (proposal is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"There is no Proposal found with this ID {id} !"
            };
        }

        _unitOfWork.proposalRepository.Delete(proposal);

        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 204, // no content
            Message = $"The Proposal with ID ({proposal.Id}) is Deleted Successfully !"
        };
    }


    public ActionResult<GeneralResponse> AcceptProposal(int proposalId)
    {
        var proposal = _unitOfWork.proposalRepository.GetById(proposalId);

        if (proposal is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"Invalid Proposal ID : {proposalId}"
            };
        }

        proposal.ApprovedTime = DateTime.Now;
        proposal.Deadline = DateTime.Now.AddDays(proposal.Duration);
        proposal.Status = ProposalStatus.Approved;

        //--------------------------------------------------------

        var job = _unitOfWork.jobRepository.GetById(proposal.JobId);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"Invalid Job ID : {proposal.JobId}"
            };
        }

        job.ApproveTime = DateTime.Now;
        job.Status = JobStatus.Closed;
        job.AcceptedFreelancerId = proposal.FreelancerId;

        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Message = $"The client {job.ClientId} Accepted the proposal {proposalId} from freelancer {job.AcceptedFreelancerId} on job {job.Id} successfully"
        };
    }


}
