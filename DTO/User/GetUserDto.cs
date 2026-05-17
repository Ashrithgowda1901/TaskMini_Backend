using System.ComponentModel.DataAnnotations;

namespace TaskMini.DTO.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public List<TaskSummaryDto> TaskSummarys { get; set; } = new List<TaskSummaryDto>();
    }
}
