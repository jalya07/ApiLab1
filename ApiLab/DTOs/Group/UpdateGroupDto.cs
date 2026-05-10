using System.ComponentModel.DataAnnotations;

namespace ApiLab.DTOs.Group;

public class UpdateGroupDto
{
    [Required(ErrorMessage = "Name can't be empty")]
    public string Name { get; set; } = string.Empty;
}