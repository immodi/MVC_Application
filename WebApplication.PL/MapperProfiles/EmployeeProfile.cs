using AutoMapper;
using WebApplicaiton.DAL.Models;
using WebApplication.PL.ViewModels;

namespace WebApplication.PL.MapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
