using System.ComponentModel.DataAnnotations;
using TaskMini.Common.Enum;

namespace TaskMini.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }=string.Empty;

        public int ProjectId {  get; set; }

        public int UserId {  get; set; }

        public TaskItemStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }
        public Project? Project { get; set; }
        public User? User { get; set; }
        
    }
}
