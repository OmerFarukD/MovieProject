using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Model.Dtos.Artists;
using MovieProject.Service.Abstracts;

namespace MovieProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController(IArtistService artistService) : ControllerBase
{

    [HttpPost("add")]
    public async Task<IActionResult> Add(ArtistAddRequestDto dto) =>Ok(await artistService.AddAsync(dto));


    [HttpPut("update")]
    public async Task<IActionResult> Update(ArtistUpdateRequestDto dto) =>Ok(await artistService.UpdateAsync(dto));

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(long id) =>Ok(await artistService.DeleteAsync(id));


    [HttpGet("getall")]
    public async Task<IActionResult> GetAll() => Ok(await artistService.GetAllAsync());


    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(long id) => Ok(await artistService.GetByIdAsync(id));

    [HttpGet("getallbymovieid")]
    public async Task<IActionResult> GetAllByMovieId(Guid id) => Ok(await artistService.GetAllByMovieIdAsync(id));
}
