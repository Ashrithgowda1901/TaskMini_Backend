namespace TaskMini.DTO.Workspace
{
    public class GetWorkspaceDto
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        
        public List<ProjectSummary> Projects { get; set; }=new List<ProjectSummary>();

    }
}
