using AutoMapper;
using Domain.Dtos.Department;
using Domain.Dtos.DepartmentEmployee;
using Domain.Dtos.DepartmentManager;
using Domain.Dtos.Employee;
using Domain.Dtos.Salary;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class ProfileService : Profile
{
    public ProfileService()
    {
        CreateMap<AddEmployee, Employee>();
        CreateMap<Employee, GetEmployee>();
        
        CreateMap<AddDepartmentDto, Department>();
        CreateMap<Department, GetDepartmentDto>();
        
        CreateMap<AddSalary, Salary>();
        CreateMap<Salary, GetSalary>();
        
        CreateMap<AddDepartmentEmployee, DepartmentEmployee>();
        CreateMap<DepartmentEmployee, GetDepartmentEmployee>();
        
        CreateMap<AddDepartmentManager, DepartmentManager>();
        CreateMap<DepartmentManager, GetDepartmentManager>();
    }
}  