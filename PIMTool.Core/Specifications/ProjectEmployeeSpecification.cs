using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class ProjectEmployeeSpecification : BaseSpecification<ProjectEmployee>
    {
        public ProjectEmployeeSpecification(int id) : base(pe => pe.Project_Id == id)
        {
            AddInclude(pe => pe.Employee);
        }
    }
}
