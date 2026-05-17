using System.ComponentModel.DataAnnotations;

namespace TaskMini.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }=string.Empty;

        public int WorkspaceId { get; set; } 

        public List<TaskItem> TaskItems { get; set; }=new List<TaskItem>();
        public Workspace? Workspace { get; set; }
    }
}
