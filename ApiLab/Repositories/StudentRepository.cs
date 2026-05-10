using ApiLab.Data;
using ApiLab.Models;
using ApiLab.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiLab.Repositories;

public class StudentRepository:IRepositories<Student>
{
    private readonly ApiLabAppDbContext _context;

    public StudentRepository(ApiLabAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
        => await _context.Students.Include(s => s.Group).ToListAsync();

    public async Task<Student?> GetByIdAsync(int id)
        => await _context.Students.Include(s => s.Group).FirstOrDefaultAsync(s => s.Id == id);

    public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        => await _context.Students.AnyAsync(s => s.Email == email && (!excludeId.HasValue || s.Id != excludeId));

    public async Task<List<Student>> SearchAsync(string? search, int page, int pageSize)
    {
        var query = _context.Students.Include(s => s.Group).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.FullName.Contains(search) || s.Email.Contains(search));

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Student entity) => await _context.Students.AddAsync(entity);
    public Task UpdateAsync(Student entity) { _context.Students.Update(entity); return Task.CompletedTask; }
    public Task DeleteAsync(Student entity) { _context.Students.Remove(entity); return Task.CompletedTask; }
    public async Task SaveAsync() => await _context.SaveChangesAsync();
    
}