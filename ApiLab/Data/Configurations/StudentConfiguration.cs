using ApiLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLab.Data.Configurations;

public class StudentConfiguration:IEntityTypeConfiguration<Student>
{

    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Age).IsRequired();

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(s => s.Email).IsUnique(); // уникальный email

        builder.Property(s => s.GroupId).IsRequired();

        builder.HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(
            new Student { Id = 1, FullName = "Ali Həsənov", Age = 20, Email = "ali@mail.com", GroupId = 1 },
            new Student { Id = 2, FullName = "Leyla Əliyeva", Age = 22, Email = "leyla@mail.com", GroupId = 1 },
            new Student { Id = 3, FullName = "Nicat Məmmədov", Age = 19, Email = "nicat@mail.com", GroupId = 2 },
            new Student { Id = 4, FullName = "Aytən Quliyeva", Age = 21, Email = "ayten@mail.com", GroupId = 2 },
            new Student { Id = 5, FullName = "Tural Rəhimov", Age = 23, Email = "tural@mail.com", GroupId = 3 }
        );
        
    }
}