using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectWithFilterForCountSpecification : BaseSpecification<Project>
    {
        public ProjectWithFilterForCountSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (string.IsNullOrEmpty(projectParams.StatusCode) || x.Status.Equals
                (projectParams.StatusCode)) &&
                (string.IsNullOrEmpty
                (projectParams.Search) || x.Project_Number.ToString().Equals
                (projectParams.Search) || x.Name.ToLower().Contains
                (projectParams.Search) || x.Customer.ToLower().Contains(projectParams.Search)
                )
            )
        {
            
        }
    }
}
