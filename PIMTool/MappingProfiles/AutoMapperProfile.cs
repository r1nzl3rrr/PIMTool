using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos;

namespace PIMTool.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.Group, o => o.MapFrom(s => s.Group.Id));

            CreateMap<Group, GroupDto>()
                .ForMember(d => d.Projects, o => o.MapFrom(s => s.Projects.Select(p => p.Project_Number)));

            CreateMap<Group, GroupWithoutProjectsDto>();
        }
    }
}
