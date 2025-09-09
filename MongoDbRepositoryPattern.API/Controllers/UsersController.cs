using Microsoft.AspNetCore.Mvc;
using MongoDbRepositoryPattern.API.Abstractions;
using MongoDbRepositoryPattern.API.DTOs;
using MongoDbRepositoryPattern.API.Models;

namespace MongoDbRepositoryPattern.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IRepository<User> _repo;

    public UsersController(IRepository<User> repo) => _repo = repo;
    
    // GET: /api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var user = await _repo.GetByIdAsync(id, ct);
        if (user is null) return NotFound();

        var result = new UserDto
        {
            Email = user.Email,
            Name = user.Name
        };

        return Ok(result);
    }

    // GET: /api/users?skip=0&take=20
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
    {
        var users = await _repo.GetAsync(null, skip, take, ct);

        var result = users.Select(u => new UserDto
        {
            Email = u.Email,
            Name = u.Name
        });

        return Ok(result);
    }

    // POST: /api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto dto, CancellationToken ct)
    {
        var user = new User
        {
            Email = dto.Email,
            Name = dto.Name
        };

        await _repo.InsertAsync(user, ct);
        return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
    }

    // PUT: /api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UserDto dto, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing is null) return NotFound();

        existing.Email = dto.Email;
        existing.Name = dto.Name;

        await _repo.ReplaceAsync(existing, ct);   // full replace
        return NoContent();
    }

    // DELETE: /api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        await _repo.DeleteAsync(id, ct);
        return NoContent();
    }
}
