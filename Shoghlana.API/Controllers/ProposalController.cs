using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProposalController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProposalController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        List<Proposal> proposals = _unitOfWork.proposal.GetAll().ToList();

        List<GetProposalDTO> getProposalsDTOs = new List<GetProposalDTO>(proposals.Count);

        foreach (var proposal in proposals)
        {
            #region Manual Mapping
            //FreelancerDTO freelancerDTO = new FreelancerDTO()
            //{
            //    Name = freelancer.Name,
            //    Address = freelancer.Address,
            //    OverView =freelancer.Overview,
            //    Title = freelancer.Title,
            //    PersonalImageBytes = freelancer.PersonalImageBytes,
            //};
            #endregion

            GetProposalDTO getProposalsDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

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
        var proposal = _unitOfWork.proposal.GetById(id);
        if (proposal == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Proposal found with this ID !"
            };
        }
        var proposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = proposalDTO,
        };
    }
    [HttpGet("JobId/{id:int}")]
    public ActionResult<GeneralResponse> GetByJobId(int id)
    {
        var proposals = _unitOfWork.proposal.GetAll().ToList();

        var getProposalDTOs = new List<GetProposalDTO>(proposals.Count);

        if (proposals == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Proposal found with this ID !"
            };
        }
        foreach (var proposal in proposals)
        {
            GetProposalDTO getProposalDTO = _mapper.Map<Proposal, GetProposalDTO>(proposal);

            getProposalDTOs.Add(getProposalDTO);

        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = getProposalDTOs,
        };
    }

    #region To Be Added

    //[HttpPost]
    //public async Task<ActionResult<GeneralResponse>> AddFreelancer([FromForm] AddFreelancerDTO addFreelancerDTO)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Data = ModelState,
    //            Message = "Invalid Data!"
    //        };
    //    }
    //    if (addFreelancerDTO.PersonalImageBytes is null)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Message = "Personal Image is required!"
    //        };
    //    }
    //    if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Message = "The allowed Personal Image Extensions => {jpg , png}",
    //        };
    //    }
    //    if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Message = "The max Allowed Personal Image Size => 1 MB ",
    //        };
    //    }

    //    using var dataStream = new MemoryStream();

    //    await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

    //    var freelancer = new Freelancer()
    //    {
    //        Name = addFreelancerDTO.Name,
    //        Title = addFreelancerDTO.Title,
    //        Address = addFreelancerDTO.Address,
    //        Overview = addFreelancerDTO.Overview,
    //        PersonalImageBytes = dataStream.ToArray(),
    //    };

    //    var addfreelancer = await _unitOfWork.freelancer.AddAsync(freelancer);

    //    _unitOfWork.Save();

    //    var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

    //    return new GeneralResponse()
    //    {
    //        IsSuccess = true,
    //        Status = 201,
    //        Data = freelancerDTO,
    //        Message = "Added Successfully"
    //    };
    //}
    //[HttpPut("{id:int}")]
    //public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddFreelancerDTO addFreelancerDTO)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Data = ModelState,
    //            Message = "Invalid Data!"
    //        };
    //    }
    //    var freelancer = _unitOfWork.freelancer.GetById(id);
    //    if (freelancer is null)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Message = "There is no Freelancer found with this ID !"
    //        };
    //    }
    //    if (addFreelancerDTO.PersonalImageBytes != null)
    //    {
    //        if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
    //        {
    //            return new GeneralResponse()
    //            {
    //                IsSuccess = false,
    //                Status = 400,
    //                Message = "The allowed Personal Image Extensions => {jpg , png}",
    //            };
    //        }

    //        if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
    //        {
    //            return new GeneralResponse()
    //            {
    //                IsSuccess = false,
    //                Status = 400,
    //                Message = "The max Allowed Personal Image Size => 1 MB ",
    //            };
    //        }
    //        using var dataStream = new MemoryStream();

    //        await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);
    //        freelancer.PersonalImageBytes = dataStream.ToArray();
    //    }
    //    freelancer.Name = addFreelancerDTO.Name;
    //    freelancer.Title = addFreelancerDTO.Title;
    //    freelancer.Overview = addFreelancerDTO.Overview;
    //    freelancer.Address = addFreelancerDTO.Address;

    //    _unitOfWork.Save();

    //    var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

    //    return new GeneralResponse()
    //    {
    //        IsSuccess = true,
    //        Status = 200,
    //        Data = freelancerDTO
    //    };
    //}
    //[HttpDelete("{id:int}")]
    //public ActionResult<GeneralResponse> Delete(int id)
    //{
    //    Freelancer? freelancer = _unitOfWork.freelancer.GetById(id);

    //    if (freelancer is null)
    //    {
    //        return new GeneralResponse()
    //        {
    //            IsSuccess = false,
    //            Status = 400,
    //            Message = "There is no Freelancer found with this ID !"
    //        };
    //    }

    //    _unitOfWork.freelancer.Delete(freelancer);

    //    _unitOfWork.Save();

    //    return new GeneralResponse()
    //    {
    //        IsSuccess = true,
    //        Status = 204, // no content
    //        Message = $"The Freelancer with ID ({freelancer.Id}) is deleted successfully !"
    //    };
    //}
    #endregion
}
