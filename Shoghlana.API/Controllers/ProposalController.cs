using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.API.Services.Interfaces;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProposalController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProposalService _proposalService;
    private readonly IProposalImageService _proposalImageService;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    
    private long maxAllowedImageSize = 1_048_576;  // 1 MB 
    public ProposalController(IMapper mapper, IProposalService proposalService, IProposalImageService proposalImageService)
    {
        _mapper = mapper;
        _proposalService = proposalService;
        _proposalImageService = proposalImageService;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var proposals = _proposalService.FindAll(new string[] { "Images" }).ToList();

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

    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var proposal = _proposalService.GetById(id);
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

    [HttpGet("JobId/{id:int}")]
    public async Task <ActionResult<GeneralResponse>> GetByJobId(int id)
    {
        var job = await _proposalService.GetByIdAsync(id);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = "There is no Job found with this ID !"
            };
        }

        var proposals = _proposalService.FindAll(includes:null, p => p.JobId == id).ToList();

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

    [HttpGet("freelancerId/{id:int}")]
    public async Task <ActionResult<GeneralResponse>> GetByFreelancerId(int id)
    {
        var freelancer = await _proposalService.GetFreelancerByIdAsync(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"There are no freelancer found with this ID {id} ."
            };
        }
        var proposals = _proposalService.FindAll(includes: null, p => p.FreelancerId == id).ToList();
       
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

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddAsync([FromForm] AddProposalDTO addProposalDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Data!"
            };
        }

        var job = await _proposalService.GetJobByIdAsync(addProposalDTO.JobId);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No job found with this ID: {addProposalDTO.JobId}!"
            };
        }

        var freelancer =await _proposalService.GetFreelancerByIdAsync(addProposalDTO.FreelancerId);
        
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

            var addProposal = await _proposalService.AddAsync(propsal);

            _proposalService.Save();

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
            var addProposal = await _proposalService.AddAsync(proposal);

            _proposalService.Save();

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
    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> Update(int id, [FromForm] AddProposalDTO addProposalDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Data!"
            };
        }
        var proposal = _proposalService.Find(p => p.Id == id, new string[] { "Images" });
        
        if (proposal is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"There is no Proposal found with this ID : {id}!"
            };
        }

        var job = await _proposalService.GetJobByIdAsync(addProposalDTO.JobId);

        if (job is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = $"No job found with this ID: {addProposalDTO.JobId}!"
            };
        }

        var freelancer = await _proposalService.GetFreelancerByIdAsync(addProposalDTO.FreelancerId);
        
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

                _proposalImageService.Delete(image);
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

            _proposalService.Save();

            var editedProposal = _proposalService.Find(p => p.Id == id,new string[] { "Images" });

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

            _proposalService.Save();

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

    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        var proposal = _proposalService.Find(criteria: p => p.Id == id, new string[] { "Images" });

        if (proposal is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"There is no Proposal found with this ID {id} !"
            };
        }

        _proposalService.Delete(proposal);

        _proposalService.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 204, // no content
            Message = $"The Proposal with ID ({proposal.Id}) is Deleted Successfully !"
        };
    }
}
