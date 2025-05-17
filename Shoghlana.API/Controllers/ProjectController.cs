using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedImageSize = 1_048_576;  // 1 MB
    public ProjectController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var projects = _unitOfWork.project.FindAll(new string[] { "Images", "Skills" });

        List<GetProjectDTO> projectDTOs = projects.Select(project => new GetProjectDTO
        {
            Title = project.Title,
            Description = project.Description,
            Poster = project.Poster,
            Link = project.Link,
            Images = project.Images?.Select(image => new GetImageDTO
            {
                Image = image.Image,
            }).ToList(),
            Skills = project.Skills?.Select(skill => new SkillDTO
            {
                Title = skill.Title,
                Description = skill.Description,
            }).ToList(),
            TimePublished = project.TimePublished,
            FreelancerId = project.FreelancerId,
        }).ToList();
        return new GeneralResponse()
        {
            Status = 200,
            IsSuccess = true,
            Data = projectDTOs
        };
    }
    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var project = _unitOfWork.project.Find(new string[] { "Skills", "Images" }, p => p.Id == id);

        if (project == null)
        {
            return new GeneralResponse()
            {
                Status = 400,
                IsSuccess = false,
                Message = "There is No Project With That ID!"
            };
        }
        GetProjectDTO projectDTO = new GetProjectDTO()
        {
            Title = project.Title,
            Description = project.Description,
            Poster = project.Poster,
            Link = project.Link,
            Skills = project.Skills?.Select(skill => new SkillDTO
            {
                Title = skill.Title,
                Description = skill.Description,
            }).ToList(),
            Images = project.Images?.Select(image => new GetImageDTO
            {
                Image = image.Image,
                ProjectId = project.Id,
            }).ToList(),
            TimePublished = project.TimePublished,
            FreelancerId = project.FreelancerId,
        };
        return new GeneralResponse()
        {
            Status = 200,
            IsSuccess = true,
            Data = projectDTO
        };
    }
    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddProject([FromForm] ProjectDTO projectDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse
            {
                Status = 400,
                IsSuccess = false,
                Message = "Model State is Invaild!"
            };
        }
        if (projectDTO.Poster == null)
        {
            return new GeneralResponse
            {
                Status = 400,
                IsSuccess = false,
                Message = "Poster is Required!"
            };
        }
        if (!allowedExtensions.Contains(Path.GetExtension(projectDTO.Poster.FileName).ToLower()))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The allowed Personal Image Extensions => {jpg , png}",
            };
        }
        if (projectDTO.Poster.Length > maxAllowedImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The max Allowed Personal Image Size => 1 MB ",
            };
        }
        using var posterDataStream = new MemoryStream();
        await projectDTO.Poster.CopyToAsync(posterDataStream);

        var projectImages = new List<ProjectImages>();

        if (projectDTO.Images != null)
        {
            foreach (var imageDTO in projectDTO.Images)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(imageDTO.Image.FileName).ToLower()))
                {
                    return new GeneralResponse
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The allowed image extensions are: jpg, png"
                    };
                }
                if (imageDTO.Image.Length > maxAllowedImageSize)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The max Allowed Personal Image Size => 1 MB ",
                    };
                }
                using var imageDataStream = new MemoryStream();

                await imageDTO.Image.CopyToAsync(imageDataStream);

                projectImages.Add(new ProjectImages { Image = imageDataStream.ToArray() });
            }
        }
        var project = new Project()
        {
            Title = projectDTO.Title,
            Description = projectDTO.Description,
            Link = projectDTO.Link,
            Poster = posterDataStream.ToArray(),
            TimePublished = projectDTO.TimePublished,
            FreelancerId = projectDTO.FreelancerId,
            Images = projectImages
        };

        await _unitOfWork.project.AddAsync(project);
        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Data = projectDTO,
            Message = "Added Successfully"
        };
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> UpdateProject(int id, [FromForm] ProjectDTO updatedProjectDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse
            {
                Status = 400,
                IsSuccess = false,
                Message = "Model State is Invaild!"
            };
        }
        var project = _unitOfWork.project.GetById(id);

        if (project == null)
        {
            return new GeneralResponse()
            {
                Status = 400,
                IsSuccess = false,
                Message = "There is No Project With That ID!"
            };
        }
        if (updatedProjectDTO.Poster != null)
        {
            if (!allowedExtensions.Contains(Path.GetExtension(updatedProjectDTO.Poster.FileName).ToLower()))
            {
                return new GeneralResponse
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The allowed image extensions are: jpg, png"
                };
            }
            if (updatedProjectDTO.Poster.Length > maxAllowedImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The max Allowed Personal Image Size => 1 MB ",
                };
            }
            using var posterDataStream = new MemoryStream();
            await updatedProjectDTO.Poster.CopyToAsync(posterDataStream);
            project.Poster = posterDataStream.ToArray();
        }

        var projectImages = new List<ProjectImages>();

        if (updatedProjectDTO.Images != null)
        {
            foreach (var imageDTO in updatedProjectDTO.Images)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(imageDTO.Image.FileName).ToLower()))
                {
                    return new GeneralResponse
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The allowed image extensions are: jpg, png"
                    };
                }
                if (imageDTO.Image.Length > maxAllowedImageSize)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Status = 400,
                        Message = "The max Allowed Personal Image Size => 1 MB ",
                    };
                }
                using var imageDataStream = new MemoryStream();

                await imageDTO.Image.CopyToAsync(imageDataStream);

                projectImages.Add(new ProjectImages { Image = imageDataStream.ToArray() });
            }
            project.Images = projectImages;
        }
        project.Title = updatedProjectDTO.Title;
        project.Description = updatedProjectDTO.Description;
        project.TimePublished = updatedProjectDTO.TimePublished;
        project.Link = updatedProjectDTO.Link;
        project.FreelancerId = updatedProjectDTO.FreelancerId;

        project.Skills ??= new List<ProjectSkills>();

        project.Skills.Clear();

        if (updatedProjectDTO.Skills != null)
        {
            project.Skills.AddRange(updatedProjectDTO.Skills.Select(skillDto => new ProjectSkills
            {
                SkillId = skillDto.Id, // Assuming the Skill already exists in DB
                ProjectId = project.Id // optional, EF can populate this
            }));
        }
        _unitOfWork.Save();

        ProjectDTO projectDTO = new ProjectDTO()
        {
            Title = updatedProjectDTO.Title,
            Description = updatedProjectDTO.Description,
            Link = updatedProjectDTO.Link,
            TimePublished = updatedProjectDTO.TimePublished,
            FreelancerId = updatedProjectDTO.FreelancerId,

            Skills = updatedProjectDTO.Skills?.Select(skill => new SkillDTO()
            {
                Title = skill.Title,
                Description = skill.Description,
            }).ToList(),

            Images = project.Images?.Select(image => new ImageDTO
            {
                Image = new FormFile(new MemoryStream(image.Image), 0,
            image.Image.Length, null, Path.GetFileName(image.Image.ToString()))
            })
            .ToList(),

            Poster = project.Poster != null ? new FormFile(new MemoryStream(project.Poster), 0, project.Poster.Length,
            null, Path.GetFileName(project.Poster.ToString())) : null,

        };

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 200,
            Data = updatedProjectDTO,
            Message = "Project updated successfully"
        };
    }

    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        var project = _unitOfWork.project.GetById(id);

        if (project == null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Project found with this ID!"
            };
        }

        _unitOfWork.project.Delete(project);
        _unitOfWork.Save();

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 200,
            Message = $"The Project with ID ({project.Id}) is deleted Successfully!"
        };
    }
}
