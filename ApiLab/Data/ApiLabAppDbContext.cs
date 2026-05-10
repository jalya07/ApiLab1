using ApiLab.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLab.Data;

public class ApiLabAppDbContext:DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public ApiLabAppDbContext(DbContextOptions<ApiLabAppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApiLabAppDbContext).Assembly);
        base.OnModelCreating(builder);
    }
    
}