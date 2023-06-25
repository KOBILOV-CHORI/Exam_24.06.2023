using System.Net;
using AutoMapper;
using Domain.Dtos.Department;
using Domain.Dtos.Employee;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DepartmentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DepartmentService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<GetDepartmentDto>> AddDepartment(AddDepartmentDto department)
    {
        try
        {
            var model = _mapper.Map<Department>(department);
            await _context.Departments.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentDto>(model);
            return new Response<GetDepartmentDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetDepartmentDto>> UpdateDepartment(AddDepartmentDto department)
    {
        try
        {
            var find = await _context.Departments.FindAsync(department.Id);
            _mapper.Map(department, find);
            _context.Entry(find).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentDto>(find);
            return new Response<GetDepartmentDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<bool>> DeleteDepartment(int id)
    {
        try
        {
            var find = await _context.Departments.FindAsync(id);
            if (find != null)
            {
                _context.Departments.Remove(find);
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }

            return new Response<bool>(false);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetDepartmentDto>> GetDepartmentById(int id)
    {
        try
        {
            var result = await (
                from d in _context.Departments
                from dm in d.DepartmentManagers
                where d.Id == id
                select new GetDepartmentDto()
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerId = dm.EmployeeId,
                    ManagerFullName = $"{dm.Employee.FirstName} {dm.Employee.LastName}"
                }).FirstOrDefaultAsync();
            return new Response<GetDepartmentDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<List<GetDepartmentDto>>> GetDepartments()
    {
        try
        {
            var result = await (
                from d in _context.Departments
                from dm in d.DepartmentManagers
                select new GetDepartmentDto()
                {
                    Id = d.Id,
                    Name = d.Name,
                    ManagerId = dm.EmployeeId,
                    ManagerFullName = $"{dm.Employee.FirstName} {dm.Employee.LastName}"
                }).ToListAsync();
            return new Response<List<GetDepartmentDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetDepartmentDto>>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }
}