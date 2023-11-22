using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Specifications
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification(EmployeeSpecParams employeeParams)
            : base(x =>
                string.IsNullOrEmpty(employeeParams.Search) || (x.First_Name + " " + x.Last_Name).ToLower().Contains
                (employeeParams.Search)
            )
        {
            AddOrderBy(x => x.First_Name + " " + x.Last_Name);
            ApplyPaging(employeeParams.PageSize * (employeeParams.PageIndex - 1),
                employeeParams.PageSize);
            if (!string.IsNullOrEmpty(employeeParams.Sort))
            {
                switch (employeeParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(e => e.First_Name + " " + e.Last_Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(e => e.First_Name + " " + e.Last_Name);
                        break;
                    default:
                        AddOrderBy(x => x.First_Name + " " + x.Last_Name);
                        break;
                }
            }
        }
    }
}
