namespace PIMTool.Core.Domain.Entities
{
    public class ProjectEmployee
    {
        public int Project_Id { get; set; }
        public Project Project { get; set; }

        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }
    }
}