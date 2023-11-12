using PIMTool.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }

        public int Group_Leader_Id { get; set; }
        public string Leader { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public IList<Project> Projects { get; set; }
    }
}
