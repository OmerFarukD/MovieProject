using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Directors;
using MovieProject.Service.Abstracts;

namespace MovieProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorsController : ControllerBase
{
    private readonly IDirectorService _directorService;

    public DirectorsController(IDirectorService directorService)
    {
        _directorService = directorService;
    }


    [HttpPost("add")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Add(DirectorAddRequestDto dto) => Ok( await _directorService.AddAsync(dto));


    [HttpPut("update")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Update(DirectorUpdateRequestDto dto) => Ok(await _directorService.UpdateAsync(dto));

    [HttpDelete("delete")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(long id) => Ok(await _directorService.DeleteAsync(id));


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll() => Ok(await _directorService.GetAllAsync());

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(long id) => Ok(await _directorService.GetByIdAsync(id));
}
