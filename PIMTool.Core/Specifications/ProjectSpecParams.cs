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
        public int? Number { get; set; }
        public string? Name 
        { 
            get => _name; 
            set => _name = value.ToLower(); 
        }
        public string? CustomerName 
        {
            get => _customerName;
            set => _customerName = value.ToLower(); 
        }
        public string? StatusCode 
        { 
            get => _statusCode; 
            set => _statusCode = value.ToLower(); 
        }
        public string? GroupLeaderVisa 
        {
            get => _groupLeaderVisa; 
            set => _groupLeaderVisa = value.ToLower(); 
        }

        private string? _name;
        private string? _customerName;
        private string? _statusCode;
        private string? _groupLeaderVisa;

        
    }
}
