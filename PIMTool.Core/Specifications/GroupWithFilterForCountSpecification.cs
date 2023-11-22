using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Specifications
{
    public class GroupWithFilterForCountSpecification : BaseSpecification<Group>
    {
        public GroupWithFilterForCountSpecification(GroupSpecParams groupParams)
            : base(x =>
                string.IsNullOrEmpty(groupParams.Search) ||
                (x.Leader.First_Name + " " + x.Leader.Last_Name).ToLower().Contains(groupParams.Search))
        {

        }
    }
}
