using System.ComponentModel.DataAnnotations;

namespace TaskMini.Entities
{
    public class Workspace
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
