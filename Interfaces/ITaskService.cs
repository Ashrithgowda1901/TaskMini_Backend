using TaskMini.DTO.Task;

namespace TaskMini.Interfaces
{
    public interface ITaskService
    {
        Task<GetTaskDto> CreateTaskAsync(CreateTaskDto task);

        Task<List<GetTaskDto>> GetTasksAsync(RequestTaskDto request);

        Task<GetTaskDto> GetTaskByIdAsync(int Id);
        Task<GetTaskDto> UpdateTaskStatusAsync(UpdateTaskStatusDto task);
    }
}
