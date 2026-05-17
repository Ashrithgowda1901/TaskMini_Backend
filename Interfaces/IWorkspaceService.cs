using TaskMini.DTO.Workspace;

namespace TaskMini.Interfaces
{
    public interface IWorkspaceService
    {
        Task<GetWorkspaceDto> CreateWorkspaceAsync(CreateWorkspaceDto workspace);

        Task<List<GetWorkspaceDto>> GetAllWorkspacesAsync();

        Task<GetWorkspaceDto> GetWorkspaceByIdAsync(int id);
    }
}
