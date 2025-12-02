using AutoMapper;
using Demo.BLL.DTOs.EmployeeDtos;
using Demo.DAL.Models.EmployeeModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.MappingProfiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            ////CreateMap<Employee, EmployeeDto>();//from Employee to EmployeeDto
            ////CreateMap<EmployeeDto, Employee>();//from EmployeeDto to Employee


            CreateMap<Employee, EmployeeDto>() 
                     .ForMember(dest => dest.EmpGender, Options => Options.MapFrom(src => src.Gender))
                     .ForMember(dest => dest.EmpType, Options => Options.MapFrom(src => src.EmployeeType))
                     .ReverseMap();

            CreateMap<Employee,EmployeeDetailsDto>()
                     .ForMember(dest => dest.Gender , options =>options.MapFrom(src=>src.Gender))
                     .ForMember(dest => dest.EmployeeType , options =>options.MapFrom(src=>src.EmployeeType))
                     .ForMember(dest => dest.HiringDate , options =>options.MapFrom(src=>DateOnly.FromDateTime(src.HiringDate)))
                     .ReverseMap();


            CreateMap<CreateEmployeeDto, Employee>()
                     .ForMember(dest => dest.HiringDate, options => options.MapFrom(src =>src.HiringDate.ToDateTime(new TimeOnly())))
                     .ReverseMap(); 
            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src =>src.HiringDate.ToDateTime(new TimeOnly())))
                .ReverseMap();
        }
    }
}
