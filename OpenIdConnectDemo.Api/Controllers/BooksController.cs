using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIdConnectDemo.Api.Dtos;
using OpenIdConnectDemo.Api.Services;

namespace OpenIdConnectDemo.Api.Controllers;

[ApiController]
[Route("books")]
[Authorize]
public class BooksController(BookService service) : ControllerBase
{
    private readonly BookService service = service;

    [HttpGet]
    [Authorize(Policy = "CanRead")]
    public async Task<IActionResult> GetAsync() => Ok(await service.GetAllAsync());

    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Policy = "CanRead")]
    public async Task<IActionResult> GetAsync([FromRoute] int id) => Ok(await service.GetByIdAsync(id));

    [HttpPost]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> Create([FromBody] BookInputDto dto)
    {
        await service.AddAsync(dto);
        return NoContent();
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] BookInputDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
