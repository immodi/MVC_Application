using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.RoleName))
                .ReverseMap();
        }
    }
}
