using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Service.Abstracts;
using MovieProject.Service.Concretes;

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
    public IActionResult Add(CategoryAddRequestDto dto)
    {
        _categoryService.Add(dto);
        return Ok("Kategori başarıyla eklendi.");
    }


    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var response = _categoryService.GetAll();
        return Ok(response);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById(int id)
    {
        var response = _categoryService.GetById(id);
        return Ok(response);
    }

    [HttpPut("update")]
    public IActionResult Update(CategoryUpdateRequestDto dto)
    {
        _categoryService.Update(dto);
        return Ok("Kategori güncellendi.");
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        _categoryService.Delete(id);
        return Ok("Kategori silindi.");
    }

}
