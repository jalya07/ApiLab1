using ApiLab.DTOs.Group;
using ApiLab.Models;
using ApiLab.Repositories;
using ApiLab.Services.Interfaces;

namespace ApiLab.Services;

public class GroupService: IGroupService
{
    private readonly GroupRepository _groupRepo;

    public GroupService(GroupRepository groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public async Task<List<GroupGetDto>> GetAllAsync()
    {
        var groups = await _groupRepo.GetAllAsync();
        return groups.Select(g => new GroupGetDto { Id = g.Id, Name = g.Name }).ToList();
    }

    public async Task<GroupGetDto?> GetByIdAsync(int id)
    {
        var g = await _groupRepo.GetByIdAsync(id);
        return g == null ? null : new GroupGetDto { Id = g.Id, Name = g.Name };
    }

    public async Task<(bool success, string message)> CreateAsync(CreateGroupDto dto)
    {
        if (await _groupRepo.NameExistsAsync(dto.Name))
            return (false, "Bu adda qrup artıq mövcuddur");

        await _groupRepo.AddAsync(new Group { Name = dto.Name });
        await _groupRepo.SaveAsync();
        return (true, "Qrup uğurla əlavə edildi");
    }

    public async Task<(bool success, string message)> UpdateAsync(int id, UpdateGroupDto dto)
    {
        var group = await _groupRepo.GetByIdAsync(id);
        if (group == null) return (false, "Qrup tapılmadı");

        if (await _groupRepo.NameExistsAsync(dto.Name, id))
            return (false, "Bu adda qrup artıq mövcuddur");

        group.Name = dto.Name;
        await _groupRepo.UpdateAsync(group);
        await _groupRepo.SaveAsync();
        return (true, "Qrup uğurla yeniləndi");
    }

    public async Task<(bool success, string message)> DeleteAsync(int id)
    {
        var group = await _groupRepo.GetByIdAsync(id);
        if (group == null) return (false, "Qrup tapılmadı");

        await _groupRepo.DeleteAsync(group);
        await _groupRepo.SaveAsync();
        return (true, "Qrup uğurla silindi");
    }
}