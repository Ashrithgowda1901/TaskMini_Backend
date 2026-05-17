namespace TaskMini.DTO.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public int UserId { get; set; }
    }
}
