using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PIMTool.Core.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string? Visa { get; set; }
        public string First_Name { get; set; } = null!;
        public string Last_Name { get; set; } = null!;
        public DateTime Birth_Date { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }
        public IList<ProjectEmployee>? ProjectEmployee { get; set; }
        
    }
}