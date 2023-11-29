using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectSpecification : BaseSpecification<Project>
    {
        public ProjectSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (!projectParams.Number.HasValue || x.Project_Number == projectParams.Number) &&
                (string.IsNullOrEmpty(projectParams.StatusCode) || x.Status.ToLower().Equals
                (projectParams.StatusCode)) &&
                ((string.IsNullOrEmpty(projectParams.Name) || x.Name.ToLower().Contains
                (projectParams.Name)) ||
                (string.IsNullOrEmpty(projectParams.CustomerName) || x.Customer.ToLower().Contains
                (projectParams.CustomerName)) ||
                (string.IsNullOrEmpty(projectParams.GroupLeaderVisa) || x.Group.Leader.Visa.ToLower().Equals
                (projectParams.GroupLeaderVisa)))
            )
        {
            AddInclude(x => x.Group);
            AddOrderBy(x => x.Name);
            ApplyPaging(projectParams.PageSize * (projectParams.PageIndex - 1),
                projectParams.PageSize);
            if (!string.IsNullOrEmpty(projectParams.Sort))
            {
                switch (projectParams.Sort)
                {
                    case "numberAsc":
                        AddOrderBy(p => p.Project_Number);
                        break;
                    case "numberDesc":
                        AddOrderByDescending(p => p.Project_Number);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProjectSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Group);
        }
    }
}