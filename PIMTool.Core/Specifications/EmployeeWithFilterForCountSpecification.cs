using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class EmployeeWithFilterForCountSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithFilterForCountSpecification(EmployeeSpecParams employeeParams)
            : base(x =>
                string.IsNullOrEmpty(employeeParams.Search) || (x.First_Name + " " + x.Last_Name).ToLower().Contains
                (employeeParams.Search)
            )
        {
        }
    }
}
