namespace ApiLab.Services.Interfaces;

public interface IStudentService
{
        Task<List<DTOs.Student.StudentGetDto>> GetAllAsync(string? search, int page, int pageSize);
        Task<DTOs.Student.StudentGetDto?> GetByIdAsync(int id);
        Task<(bool success, string message)> CreateAsync(DTOs.Student.CreateStudentDto dto);
        Task<(bool success, string message)> UpdateAsync(int id, DTOs.Student.UpdateStudentDto dto);
        Task<(bool success, string message)> DeleteAsync(int id);
        
}