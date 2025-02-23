using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Service.Abstracts;
using MovieProject.Service.Concretes;


// RFC 7807
namespace MovieProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    // Constructor arg injection
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // property Injection 
    //public ICategoryService CategoryService { get; set; }
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CategoryAddRequestDto dto)
    { 
            await _categoryService.AddAsync(dto);
            return Ok("Kategori başarıyla eklendi.");
      
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("getbyid")]
    
    public async Task<IActionResult> GetById(int id)
    {
   
            var response = await _categoryService.GetByIdAsync(id);
            return Ok(response);
        
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(CategoryUpdateRequestDto dto)
    {


            await _categoryService.UpdateAsync(dto);
            return Ok("Kategori güncellendi.");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
            await _categoryService.DeleteAsync(id);
            return Ok("Kategori silindi.");
    }

}
