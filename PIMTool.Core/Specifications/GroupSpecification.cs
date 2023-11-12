using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class GroupSpecification : BaseSpecification<Group>
    {
        public GroupSpecification() 
        {
            AddInclude(g => g.Leader);
        }
        public GroupSpecification(int id) : base(g => g.Id == id)
        {
            AddInclude(g => g.Leader);
            AddInclude(g => g.Projects);
        }
    }
}
