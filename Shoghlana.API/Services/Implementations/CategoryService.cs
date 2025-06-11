using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class CategoryService : GenericService<Category>, ICategoryService
{
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository, IMapper mapper)
        : base(unitOfWork, repository)
    {
        _mapper = mapper;
    }


    public ActionResult<GeneralResponse> GetAll()
    {
        var categories = _unitOfWork.categoryRepository.FindAll();

        if (categories != null)
        {
            var categoryDTOs = new List<CategoryDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                categoryDTOs.Add(categoryDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
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


    public ActionResult<GeneralResponse> GetById(int id)
    {
        var category = _unitOfWork.categoryRepository.GetById(id);

        if (category != null)
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


    public ActionResult<GeneralResponse> CreateCategory(GetTitleOfCategoryDTO getTitleofCategoryDTO)
    {
        var category = _mapper.Map<Category>(getTitleofCategoryDTO);

        _unitOfWork.categoryRepository.Add(category);
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


    public ActionResult<GeneralResponse> DeleteCategory(int id)
    {
        var category = _unitOfWork.categoryRepository.GetById(id);

        if (category != null)
        {
            _unitOfWork.categoryRepository.Delete(category);
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


    public ActionResult<GeneralResponse> UpdateCategory(int id, GetTitleOfCategoryDTO getTitleofCategoryDTO)
    {
        Category? category = _unitOfWork.categoryRepository.GetById(id);

        if (category != null)
        {
            _mapper.Map(getTitleofCategoryDTO, category);
            _unitOfWork.categoryRepository.Update(category);
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

}
