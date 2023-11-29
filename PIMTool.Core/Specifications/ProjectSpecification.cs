using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectSpecification : BaseSpecification<Project>
    {
        
        public ProjectSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (string.IsNullOrEmpty(projectParams.StatusCode) || x.Status.Equals
                (projectParams.StatusCode)) &&
                (string.IsNullOrEmpty
                (projectParams.Search) || x.Project_Number.ToString().Equals
                (projectParams.Search) || x.Name.ToLower().Contains
                (projectParams.Search) || x.Customer.ToLower().Contains
                (projectParams.Search) || x.Group.Leader.Visa.ToLower().Equals(projectParams.Search)
                )
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