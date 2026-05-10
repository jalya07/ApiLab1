namespace ApiLab.DTOs.Student;

public class StudentGetDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public int GroupId { get; set; }
    public string GroupName { get; set; } = null!;
}