namespace ApiLab.Services.Interfaces;

public interface IGroupService
{
    Task<List<DTOs.Group.GroupGetDto>> GetAllAsync();
    Task<DTOs.Group.GroupGetDto?> GetByIdAsync(int id);
    Task<(bool success, string message)> CreateAsync(DTOs.Group.CreateGroupDto dto);
    Task<(bool success, string message)> UpdateAsync(int id, DTOs.Group.UpdateGroupDto dto);
    Task<(bool success, string message)> DeleteAsync(int id);
}