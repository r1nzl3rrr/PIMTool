using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectWithFilterForCountSpecification : BaseSpecification<Project>
    {
        public ProjectWithFilterForCountSpecification(ProjectSpecParams projectParams)
            : base(x =>
                (string.IsNullOrEmpty(projectParams.SearchProjectName) || x.Name.ToLower().Contains
                (projectParams.SearchProjectName)) &&
                (string.IsNullOrEmpty(projectParams.SearchCustomerName) || x.Customer.ToLower().Contains
                (projectParams.SearchCustomerName)) &&
                (string.IsNullOrEmpty(projectParams.SearchProjectStatusCode) || x.Status.ToLower().Equals
                (projectParams.SearchProjectStatusCode)) &&
                (string.IsNullOrEmpty(projectParams.SearchGroupLeaderVisa) || x.Group.Leader.Visa.ToLower().Equals
                (projectParams.SearchGroupLeaderVisa))
            )
        {

        }
    }
}
