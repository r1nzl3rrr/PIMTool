using PIMTool.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }

        public int Group_Leader_Id { get; set; }
        public string Leader { get; set; }
        public IList<int> Projects { get; set; }
    }
}
