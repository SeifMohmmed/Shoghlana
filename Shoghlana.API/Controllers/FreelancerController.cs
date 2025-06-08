using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;
using Shoghlana.API.Services.Interfaces;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FreelancerController : ControllerBase
{
    private readonly IFreelancerService _freelancerService;
    private readonly IMapper _mapper;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedPersonalImageSize = 1_048_576;  // 1 MB = 1024 * 1024 bytes

    public FreelancerController(IFreelancerService freelancerService, IMapper mapper)
    {
        _freelancerService = freelancerService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var freelancers = _freelancerService.FindAll(includes: new[] { "Skills" }).ToList();

        var freelancerDTOs = new List<FreelancerDTO>(freelancers.Count());

        foreach (var freelancer in freelancers)
        {
            var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

            freelancerDTOs.Add(freelancerDTO);
        }
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTOs
        };
    }

    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var freelancer = _freelancerService.Find(criteria:null,includes: new[] { "Skills" });

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 404,
                Message = "There is no Freelancer found with this ID !"

            };
        }
        var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTO
        };
    }

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddFreelancer([FromForm] AddFreelancerDTO addFreelancerDTO)
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

        if (addFreelancerDTO.PersonalImageBytes is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Personal Image is required!"
            };
        }

        if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The allowed Personal Image Extensions => {jpg , png}",
            };
        }

        if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The max Allowed Personal Image Size => 1 MB ",
            };
        }

        using var dataStream = new MemoryStream();

        await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);

        var freelancer = new Freelancer()
        {
            Name = addFreelancerDTO.Name,
            Title = addFreelancerDTO.Title,
            Address = addFreelancerDTO.Address,
            Overview = addFreelancerDTO.Overview,
            PersonalImageBytes = dataStream.ToArray(),
        };

        var addfreelancer = await _freelancerService.AddAsync(freelancer);

        _freelancerService.Save();

        var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 201,
            Data = freelancerDTO,
            Message = "Added Successfully"
        };
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddFreelancerDTO addFreelancerDTO)
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

        var freelancer = _freelancerService.GetById(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"
            };
        }

        if (addFreelancerDTO.PersonalImageBytes != null)
        {
            if (!allowedExtensions.Contains(Path.GetExtension(addFreelancerDTO.PersonalImageBytes.FileName).ToLower()))
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The allowed Personal Image Extensions => {jpg , png}",
                };
            }

            if (addFreelancerDTO.PersonalImageBytes.Length > maxAllowedPersonalImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The max Allowed Personal Image Size => 1 MB ",
                };
            }

            using var dataStream = new MemoryStream();

            await addFreelancerDTO.PersonalImageBytes.CopyToAsync(dataStream);
            
            freelancer.PersonalImageBytes = dataStream.ToArray();

        }
        #region Don't use Automapper here
        // can't use update here because the same instance is already tracked when I got him by Id
        // so I just map with my self and save changes => also cant use automapper because it creates
        // a new instance and doesn't modify the existed one 
        // so SaveChanges won't take effect unless I Mapped manually 
        #endregion

        freelancer.Name = addFreelancerDTO.Name;
        freelancer.Title = addFreelancerDTO.Title;
        freelancer.Overview = addFreelancerDTO.Overview;
        freelancer.Address = addFreelancerDTO.Address;

        _freelancerService.Save();

        var freelancerDTO = _mapper.Map<Freelancer, FreelancerDTO>(freelancer);

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = freelancerDTO
        };
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        Freelancer? freelancer = _freelancerService.GetById(id);

        if (freelancer is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Freelancer found with this ID !"
            };
        }

        _freelancerService.Delete(freelancer);

        _freelancerService.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 204, // no content
            Message = $"The Freelancer with ID ({freelancer.Id}) is deleted successfully !"
        };
    }
}
