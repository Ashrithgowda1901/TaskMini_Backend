using TaskMini.Common.Enum;

namespace TaskMini.DTO.Task
{
    public class UpdateTaskStatusDto
    {
        public int Id { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
