using TaskMini.DTO.User;

namespace TaskMini.DTO.Project
{
    public class GetProjectDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int WorkspaceId { get; set; }


        public List<TaskSummaryDto> TaskItems { get; set; } = new List<TaskSummaryDto>();

    }
}
