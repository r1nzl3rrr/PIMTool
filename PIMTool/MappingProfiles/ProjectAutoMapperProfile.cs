using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos;

namespace PIMTool.MappingProfiles
{
    public class ProjectAutoMapperProfile : Profile
    {
        public ProjectAutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.Group, o => o.MapFrom(s => s.Group.Id));
        }
    }
}