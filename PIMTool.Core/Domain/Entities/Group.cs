using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMTool.Core.Domain.Entities 
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public int Group_Leader_Id { get; set; }
        public Employee Leader { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public IList<Project> Projects { get; set; }

    }
}