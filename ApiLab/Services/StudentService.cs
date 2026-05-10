using ApiLab.DTOs.Student;
using ApiLab.Models;
using ApiLab.Repositories;
using ApiLab.Services.Interfaces;

namespace ApiLab.Services;

public class StudentService:IStudentService
{
 private readonly StudentRepository _studentRepo;
    private readonly GroupRepository _groupRepo;

    public StudentService(StudentRepository studentRepo, GroupRepository groupRepo)
    {
        _studentRepo = studentRepo;
        _groupRepo = groupRepo;
    }

    public async Task<List<StudentGetDto>> GetAllAsync(string? search, int page, int pageSize)
    {
        var students = await _studentRepo.SearchAsync(search, page, pageSize);
        return students.Select(s => new StudentGetDto
        {
            Id = s.Id,
            FullName = s.FullName,
            Age = s.Age,
            Email = s.Email,
            GroupId = s.GroupId,
            GroupName = s.Group?.Name ?? ""
        }).ToList();
    }

    public async Task<StudentGetDto?> GetByIdAsync(int id)
    {
        var s = await _studentRepo.GetByIdAsync(id);
        if (s == null) return null;
        return new StudentGetDto
        {
            Id = s.Id, FullName = s.FullName, Age = s.Age,
            Email = s.Email, GroupId = s.GroupId, GroupName = s.Group?.Name ?? ""
        };
    }

    public async Task<(bool success, string message)> CreateAsync(CreateStudentDto dto)
    {
        var groupExists = await _groupRepo.GetByIdAsync(dto.GroupId);
        if (groupExists == null) return (false, "Belə bir qrup mövcud deyil");

        if (await _studentRepo.EmailExistsAsync(dto.Email))
            return (false, "Bu email artıq istifadə olunur");

        var student = new Student
        {
            FullName = dto.FullName, Age = dto.Age,
            Email = dto.Email, GroupId = dto.GroupId
        };

        await _studentRepo.AddAsync(student);
        await _studentRepo.SaveAsync();
        return (true, "Tələbə uğurla əlavə edildi");
    }

    public async Task<(bool success, string message)> UpdateAsync(int id, UpdateStudentDto dto)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null) return (false, "Tələbə tapılmadı");

        var groupExists = await _groupRepo.GetByIdAsync(dto.GroupId);
        if (groupExists == null) return (false, "Belə bir qrup mövcud deyil");

        if (await _studentRepo.EmailExistsAsync(dto.Email, id))
            return (false, "Bu email artıq istifadə olunur");

        student.FullName = dto.FullName;
        student.Age = dto.Age;
        student.Email = dto.Email;
        student.GroupId = dto.GroupId;

        await _studentRepo.UpdateAsync(student);
        await _studentRepo.SaveAsync();
        return (true, "Tələbə uğurla yeniləndi");
    }

    public async Task<(bool success, string message)> DeleteAsync(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null) return (false, "Tələbə tapılmadı");

        await _studentRepo.DeleteAsync(student);
        await _studentRepo.SaveAsync();
        return (true, "Tələbə uğurla silindi");
    }
}