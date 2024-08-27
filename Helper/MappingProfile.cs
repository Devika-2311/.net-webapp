using AssignmentWebApi.Dto;
using AssignmentWebApi.Helper;
using AssignmentWebApi.Models;
using AutoMapper;
namespace AssignmentWebApi.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<Employee, EmployeeDto>()
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
            CreateMap<EmployeeDto, Employee>();
           
            CreateMap<Employee, EmployeeEdit>().ReverseMap();

            
            CreateMap<EmployeeDto, EmployeeEdit>().ReverseMap();
            CreateMap<SalarySlip, SalaryDto>();
            CreateMap<Employee, LoginDto>();
        }
    }
}
