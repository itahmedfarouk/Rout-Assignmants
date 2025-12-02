using AutoMapper;
using Demo.BL2.DTOS.Employee;
using Demo.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL2.Mapping_Profiles
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<Employee,EmployeeDto>()
				.ForMember(dest => dest.department, opt => opt.MapFrom(src => src.Department.Name ?? null))
				.ReverseMap();
			CreateMap<Employee,CreatedEmployeeDto>().ReverseMap();
			CreateMap<Employee,EmployeeDetailsDto>()
				.ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
				.ForMember(dest => dest.department, opt => opt.MapFrom(src => src.Department.Name ?? null))
				.ReverseMap();
			CreateMap<CreatedEmployeeDto,Employee>().ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly()))).ReverseMap();
			CreateMap<UpdatedEmployeeDto,Employee>().ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly()))).ReverseMap();



		}

	}
}
