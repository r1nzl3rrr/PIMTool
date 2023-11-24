using PIMTool.Core.Validation;

namespace PIMTool.AddingAndUpdatingDtos
{
    public class AddingAndUpdatingProjectDto
    {
        public int Group_Id { get; set; }

        public int Project_Number { get; set; }

        public string Name { get; set; } = null!;

        public string Customer { get; set; } = null!;

        [ProjectStatus]
        public string Status { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
    }
}
