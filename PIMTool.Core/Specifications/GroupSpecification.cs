using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class GroupSpecification : BaseSpecification<Group>
    {
        public GroupSpecification(GroupSpecParams groupParams)
            : base(x =>
                string.IsNullOrEmpty(groupParams.Search) || 
                (x.Leader.First_Name + " " + x.Leader.Last_Name).ToLower().Contains(groupParams.Search))
        {
            AddInclude(g => g.Leader);
            AddInclude(g => g.Projects);
            AddOrderBy(x => x.Id);
            ApplyPaging(groupParams.PageSize * (groupParams.PageIndex - 1),
                groupParams.PageSize);
            if (!string.IsNullOrEmpty(groupParams.Sort))
            {
                switch (groupParams.Sort)
                {
                    case "idAsc":
                        AddOrderBy(g => g.Id);
                        break;
                    case "idDesc":
                        AddOrderByDescending(g => g.Id);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
        public GroupSpecification(int id) : base(g => g.Id == id)
        {
            AddInclude(g => g.Leader);
            AddInclude(g => g.Projects);
        }
    }
}
