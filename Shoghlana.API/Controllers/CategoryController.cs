using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var categories = _unitOfWork.category.GetAll();
        if (categories != null)
        {
            var categoryDTOs = new List<GetTitleOfCategoryDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = _mapper.Map<GetTitleOfCategoryDTO>(category);

                categoryDTOs.Add(categoryDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status=200,
                Data = categoryDTOs
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "There is no categories"
        };
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var category = _unitOfWork.category.GetById(id);
        if(category != null)
        {
            var categoryDTO = _mapper.Map<CategoryDTO>(category);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = categoryDTO
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "Category Not Found !"
        };
    }


    [HttpPost]
    public ActionResult<GeneralResponse> AddCategory(GetTitleOfCategoryDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess=false,
                Status = 400,
                Message="Invalid Data !"
            };
        }

        var category = _mapper.Map<Category>(dto);
        _unitOfWork.category.Add(category);
        _unitOfWork.Save();

        var categoryDTO = _mapper.Map<CategoryDTO>(category);
        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 201,
            Data = categoryDTO,
            Message = "Category Created Successfully"
        };
    }


    [HttpPut("{id:int}")]
    public ActionResult<GeneralResponse> UpdateCategory(int id, GetTitleOfCategoryDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Invalid Data !"
            };
        }

        var category = _unitOfWork.category.GetById(id);

        if (category != null)
        {
            _mapper.Map(dto,category);
            _unitOfWork.category.Update(category);
            _unitOfWork.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Message = "Category Updated Successfully !"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 404,
            Message = "Category Not Found !"
        };

    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> DeleteCategory(int id)
    {

        var category = _unitOfWork.category.GetById(id);

        if (category != null)
        {
            _unitOfWork.category.Delete(category);
            _unitOfWork.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Message = "Category Deleted Successfully !"
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 404,
            Message = "Category Not Found !"
        };
    }
}
