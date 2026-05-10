using ApiLab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiLab.Data.Configurations;

public class GroupConfiguration:IEntityTypeConfiguration<Group>
{
     public void Configure(EntityTypeBuilder<Group> builder)
     {
          builder.HasKey(s => s.Id);
          builder.Property(s => s.Id).ValueGeneratedOnAdd();

          builder.Property(g => g.Name)
               .IsRequired()
               .HasMaxLength(50);

          builder.HasIndex(g => g.Name).IsUnique();
          
          builder.HasData(
               new Group { Id = 1, Name = "Group A" },
               new Group { Id = 2, Name = "Group B" },
               new Group { Id = 3, Name = "Group C" }
          );
          
     }
}