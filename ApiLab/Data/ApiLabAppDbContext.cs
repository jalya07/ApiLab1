using Microsoft.EntityFrameworkCore;

namespace ApiLab.Data;

public class ApiLabAppDbContext:DbContext
{
    public ApiLabAppDbContext(DbContextOptions<ApiLabAppDbContext> options) : base(options)
    {
        
    }
    
    
}