using System.ComponentModel.DataAnnotations;

namespace ApiLab.DTOs.Student;

public class CreateStudentDto
{
    [Required(ErrorMessage = "FullName can't be empty")]
    [MinLength(3, ErrorMessage = "FullName must be at least 3 characters long")]
    public string FullName { get; set; } = string.Empty;

    [Range(16, 60, ErrorMessage = "Age must be between 16 and 60")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Email can't be empty")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int GroupId { get; set; }
}