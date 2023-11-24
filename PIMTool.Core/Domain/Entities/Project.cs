using PIMTool.Core.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMTool.Core.Domain.Entities
{
    public class Project : BaseEntity
    {
        public int? Group_Id { get; set; }
        public Group? Group { get; set; }
        
        public int Project_Number { get; set; }

        public string Name { get; set; } = null!;

        public string Customer { get; set; } = null!;

        [ProjectStatus]
        public string Status { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public IList<ProjectEmployee>? ProjectEmployee {  get; set; }
    }
}