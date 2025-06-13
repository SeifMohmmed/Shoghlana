using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class ProjectService : GenericService<Project>, IProjectService
{
    private readonly IMapper _mapper;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedImageSize = 1_048_576;  // 1 MB

    public ProjectService(IUnitOfWork unitOfWork, IGenericRepository<Project> repository, IMapper mapper)
        : base(unitOfWork, repository)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetByFreelancerId(int id)
    {
        var projects = _unitOfWork.projectRepository
            .FindAll(new  string []{"Images","Skills"},p=>p.FreelancerId==id).ToList();

        if(projects is null || projects.Count== 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = null,
                Message = $"No Projects found for this freelancer (ID = {id})",
                Status = 400,
            };
        }

        var projectDTOs= new List<GetProjectDTO>();

        foreach(var project in projects)
        {
            var projectDTO = _mapper.Map<GetProjectDTO>(project);

            foreach(var skill in project.Skills)
            {
                var skillDTO = _mapper.Map<SkillDTO>(skill);

                projectDTO.Skills.Add(skillDTO);
            }

            foreach(var image in project.Images)
            {
                var imageDTO = _mapper.Map<GetImageDTO>(image);

                projectDTO.Images.Add(imageDTO);
            }

            projectDTO.Poster = project.Poster;

            projectDTOs.Add(projectDTO);
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Data = projectDTOs,
            Status = 200,
            Message = $"The projects done by the freelancer with ID = {id}"
        };
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var projects = _unitOfWork.projectRepository.FindAll(new string[] { "Images", "Skills" });

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
                Title = _unitOfWork.skillRepository.GetById(skill.SkillId).Title,
                Description = _unitOfWork.skillRepository.GetById(skill.SkillId).Description,
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
        var project = _unitOfWork.projectRepository.Find(p => p.Id == id, new string[] { "Skills", "Images" });

        if (project == null)
        {
            return new GeneralResponse()
            {
                Status = 400,
                IsSuccess = false,
                Message = "There is No Project With That ID!"
            };
        }

        var projectDTO = new GetProjectDTO()
        {
            Title = project.Title,
            Description = project.Description,
            Poster = project.Poster,
            Link = project.Link,

            Skills = project.Skills?.Select(skill => new SkillDTO
            {
                Title = _unitOfWork.skillRepository.GetById(skill.SkillId).Title,
                Description = _unitOfWork.skillRepository.GetById(skill.SkillId).Description,
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
    public async Task<ActionResult<GeneralResponse>> AddAsync([FromForm] ProjectDTO projectDTO)
    {
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
                Message = "The allowed image extensions for poster are: jpg, png",
            };
        }

        if (projectDTO.Poster.Length > maxAllowedImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The maximum allowed poster size is 1 MB",
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
                        Message = "The maximum allowed image size is 1 MB",
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

        project.Skills = projectDTO.Skills?.Select(skillDTO => new ProjectSkills   // skills are added after project id is generated
        {
           // SkillId = skillDTO.Id,
            ProjectId = project.Id
        }).ToList();

        _unitOfWork.projectRepository.Update(project);
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
    public async Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] ProjectDTO updatedProjectDTO)
    {
        var project = _unitOfWork.projectRepository.GetById(id);

        if (project == null)
        {
            return new GeneralResponse()
            {
                Status = 400,
                IsSuccess = false,
                Message = "There is no Project Found With This ID!"
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
                    Message = "The allowed image extensions for poster are: jpg, png"
                };
            }

            if (updatedProjectDTO.Poster.Length > maxAllowedImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The maximum allowed poster size is 1 MB",
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
                        Message = "The maximum allowed poster size is 1 MB",
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
               // SkillId = skillDto.Id,
                ProjectId = project.Id
            }));
        }

        _unitOfWork.Save();

        var projectDTO = new ProjectDTO()
        {
            Title = updatedProjectDTO.Title,
            Description = updatedProjectDTO.Description,
            Link = updatedProjectDTO.Link,
            TimePublished = updatedProjectDTO.TimePublished,
            FreelancerId = updatedProjectDTO.FreelancerId,

            Skills = updatedProjectDTO.Skills?.Select(skill => new SkillDTO()
            {
                Title = _unitOfWork.skillRepository.GetById(skill.Id).Title,
                Description = _unitOfWork.skillRepository.GetById(skill.Id).Description,
            }).ToList(),

            Images = project.Images?.Select(image => new ImageDTO
            {
                Image = new FormFile(new MemoryStream(image.Image), 0,
            image.Image.Length, null, Path.GetFileName(image.Image.ToString()))
            }).ToList(),


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
        var project = _unitOfWork.projectRepository.GetById(id);

        if (project == null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Status = 400,
                Message = "There is no Project found with this ID!"
            };
        }

        _unitOfWork.projectRepository.Delete(project);
        _unitOfWork.Save();

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 200,
            Message = $"The Project with ID ({project.Id}) is Deleted Successfully!"
        };

    }


}
