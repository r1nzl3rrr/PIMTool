using AutoMapper;
using PIMTool.Core.Domain.Entities;
using PIMTool.Dtos;


namespace PIMTool.MappingProfiles
{
    public class GroupAutoMapperProfile : Profile
    {
        public GroupAutoMapperProfile()
        {
            CreateMap<Group, GroupDto>()
                .ForMember(d => d.Leader, o => o.MapFrom(s => s.Leader.First_Name + " " + s.Leader.Last_Name));
        }
    }
}
