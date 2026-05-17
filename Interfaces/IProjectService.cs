using TaskMini.DTO.Project;

namespace TaskMini.Interfaces
{
    public interface IProjectService
    {
        Task<GetProjectDto> CreateProjectAsync(CreateProjectDto dto);

        Task<List<GetProjectDto>> GetProjectsAsync();

        Task<GetProjectDto> GetProjectByIdAsync(int id);
    }
}
