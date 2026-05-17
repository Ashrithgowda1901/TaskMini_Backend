using System.ComponentModel.DataAnnotations;
using TaskMini.DTO.User;

namespace TaskMini.DTO.Workspace
{
    public class ProjectSummary
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;


        public List<TaskSummaryDto> TaskItems { get; set; } = new List<TaskSummaryDto>();
    }
}
