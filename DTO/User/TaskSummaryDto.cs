using System.ComponentModel.DataAnnotations;
using TaskMini.Common.Enum;

namespace TaskMini.DTO.User
{
    public class TaskSummaryDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }
    }
}
