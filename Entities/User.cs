using System.ComponentModel.DataAnnotations;

namespace TaskMini.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; }=string.Empty;

        public string PasswordHash {  get; set; } = string.Empty;

        public List<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
