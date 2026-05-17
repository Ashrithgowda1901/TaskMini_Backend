using System.ComponentModel.DataAnnotations;

namespace TaskMini.DTO.User
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
