using System.ComponentModel.DataAnnotations;
using PIMTool.Core.Validation;

namespace PIMTool.AddingAndUpdatingDtos
{
    public class AddingAndUpdatingProjectDto
    {
        [Required]
        [Range(1, double.MaxValue)]
        public int Group_Id { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public int Project_Number { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Customer { get; set; } = null!;

        [ProjectStatus]
        public string Status { get; set; } = null!;
        [Required]
        public DateTime Start_Date { get; set; }
        [Required]
        public DateTime End_Date { get; set; }
    }
}
