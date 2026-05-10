using ApiLab.DTOs.Group;
using ApiLab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : Controller
{
   
    private readonly IGroupService _service;

    public GroupsController(IGroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound("Qrup tapılmadı") : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, message) = await _service.CreateAsync(dto);
        return success ? Ok(message) : BadRequest(message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGroupDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, message) = await _service.UpdateAsync(id, dto);
        return success ? Ok(message) : BadRequest(message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var (success, message) = await _service.DeleteAsync(id);
        return success ? Ok(message) : NotFound(message);
    }
}