using AutoMapper;
using WebApplicaiton.DAL.Models;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
