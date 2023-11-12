using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectsWithGroupsSpecification : BaseSpecification<Project>
    {
        public ProjectsWithGroupsSpecification()
        {
            AddInclude(x => x.Group);
        }
        
        public ProjectsWithGroupsSpecification(int id) : base(x => x.Id == id) 
        {
            AddInclude(x => x.Group);
        }

        public ProjectsWithGroupsSpecification(string projectName, string customerName, string projectStatus) : base(x => x.Name == projectName && x.Customer == customerName && x.Status == projectStatus)
        {
            AddInclude(x => x.Group);
        }

        public ProjectsWithGroupsSpecification(string projectName, string customerName, string projectStatus, string groupLeaderVisa) : base(x => x.Name == projectName && x.Customer == customerName && x.Status == projectStatus && x.Group.Leader.Visa == groupLeaderVisa)
        {
            AddInclude(x => x.Group);
        }
    }
}