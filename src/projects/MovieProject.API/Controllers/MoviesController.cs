using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Movies;
using MovieProject.Service.Abstracts;

namespace MovieProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }


    [HttpPost("Add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add(MovieAddRequestDto dto)
    {
        var result = await _movieService.AddAsync(dto);

        return Ok(result);
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _movieService.GetAllAsync();
        return Ok(result);
    }


    [HttpGet("getallbycategoryid")]
    public async Task<IActionResult> GetAllByCategoryId(int categoryId)
    {
        var result = await _movieService.GetAllByCategoryIdAsync(categoryId);
        return Ok(result);
    }

    [HttpGet("getallbydirectorid")]
    public async Task<IActionResult> GetAllByDirectorId(long directorId)
    {
        var result = await _movieService.GetAllByDirectorIdAsync(directorId);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(MovieUpdateRequestDto dto)
    {
        await _movieService.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _movieService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await _movieService.GetByIdAsync(id));

    [HttpGet("imdbrange")]
    public async Task<IActionResult> GetAllByImdbRange(double min, double max)
        => Ok(await _movieService.GetAllByImdbRangeAsync(min,max));

}
