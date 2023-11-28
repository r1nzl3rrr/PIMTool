using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectWithFilterForCountSpecification : BaseSpecification<Project>
    {
        public ProjectWithFilterForCountSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (!projectParams.Number.HasValue || x.Project_Number == projectParams.Number) &&
                (string.IsNullOrEmpty(projectParams.Name) || x.Name.ToLower().Contains
                (projectParams.Name)) &&
                (string.IsNullOrEmpty(projectParams.CustomerName) || x.Customer.ToLower().Contains
                (projectParams.CustomerName)) &&
                (string.IsNullOrEmpty(projectParams.StatusCode) || x.Status.ToLower().Equals
                (projectParams.StatusCode)) &&
                (string.IsNullOrEmpty(projectParams.GroupLeaderVisa) || x.Group.Leader.Visa.ToLower().Equals
                (projectParams.GroupLeaderVisa))
            )
        {

        }
    }
}
