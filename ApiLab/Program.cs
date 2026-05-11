using ApiLab.Data;
using ApiLab.Repositories;
using ApiLab.Services;
using ApiLab.Services.Interfaces;
using ApiLab.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ApiLab;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        // Add services to the container.
        builder.Services.AddDbContext<ApiLabAppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<StudentRepository>();
        builder.Services.AddScoped<GroupRepository>();

// Services
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<IGroupService, GroupService>();
        
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<StudentCreateDTOValidation>();
        builder.Services.AddValidatorsFromAssemblyContaining<StudentUpdateDTOValidation>();
        
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        
            app.UseSwagger();
            app.UseSwaggerUI();
        
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        await context.Response.WriteAsync($"{{ \"error\": \"{error.Error.Message}\" }}");
                    }
                });
            });
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}