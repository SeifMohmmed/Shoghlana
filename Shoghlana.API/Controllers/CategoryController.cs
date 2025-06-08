using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _categoryService.GetAll();
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _categoryService.GetById(id);
    }


    [HttpPost]
    public ActionResult<GeneralResponse> AddCategory(GetTitleOfCategoryDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Invalid data!"
            };
        }

        return _categoryService.CreateCategory(dto);
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
                Message = "Invalid data!"
            };
        }

        return _categoryService.UpdateCategory(id, dto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> DeleteCategory(int id)
    {
        return _categoryService.DeleteCategory(id);
    }
}
