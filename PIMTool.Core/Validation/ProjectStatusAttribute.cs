

using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Validation
{
    public class ProjectStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value == null) return false;
            var status = value.ToString();
            return status == "NEW" || status == "PLA" || status == "INP" || status == "FIN";
        }
        
    }
}
