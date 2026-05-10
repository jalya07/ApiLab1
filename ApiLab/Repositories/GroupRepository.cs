using ApiLab.Data;
using ApiLab.Models;
using ApiLab.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiLab.Repositories;

public class GroupRepository:IRepositories<Group>
{
    private readonly ApiLabAppDbContext _context;

    public GroupRepository(ApiLabAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Group>> GetAllAsync()
        => await _context.Groups.ToListAsync();

    public async Task<Group?> GetByIdAsync(int id)
        => await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        => await _context.Groups.AnyAsync(g => g.Name == name && (!excludeId.HasValue || g.Id != excludeId));

    public async Task AddAsync(Group entity) => await _context.Groups.AddAsync(entity);
    public Task UpdateAsync(Group entity) { _context.Groups.Update(entity); return Task.CompletedTask; }
    public Task DeleteAsync(Group entity) { _context.Groups.Remove(entity); return Task.CompletedTask; }
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}