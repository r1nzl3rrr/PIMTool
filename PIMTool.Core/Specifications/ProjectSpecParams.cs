using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Specifications
{
    public class ProjectSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        
        public string? Sort { get; set; }
        public string? SearchProjectName 
        { 
            get => _searchProjectName; 
            set => _searchProjectName = value.ToLower(); 
        }
        public string? SearchCustomerName 
        {
            get => _searchCustomerName;
            set => _searchCustomerName = value.ToLower(); 
        }
        public string? SearchProjectStatusCode 
        { 
            get => _searchProjectStatusCode; 
            set => _searchProjectStatusCode = value.ToLower(); 
        }
        public string? SearchGroupLeaderVisa 
        {
            get => _searchGroupLeaderVisa; 
            set => _searchGroupLeaderVisa = value.ToLower(); 
        }

        private string? _searchProjectName;
        private string? _searchCustomerName;
        private string? _searchProjectStatusCode;
        private string? _searchGroupLeaderVisa;

        
    }
}
