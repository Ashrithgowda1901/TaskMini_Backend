using System.Collections.Generic;
using TaskMini.Common.Enum;
using TaskMini.DTO.User;

namespace TaskMini.DTO.Task
{

    public class GetTaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }
    }

}
