using System.Net;
using AutoMapper;
using Domain.Dtos.DepartmentEmployee;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DepartmentEmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DepartmentEmployeeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<GetDepartmentEmployee>> AddDepartmentEmployee(AddDepartmentEmployee departmentEmployee)
    {
        try
        {
            departmentEmployee.FromDate = DateTime.SpecifyKind(departmentEmployee.FromDate, DateTimeKind.Utc);
            departmentEmployee.ToDate = DateTime.SpecifyKind(departmentEmployee.ToDate, DateTimeKind.Utc);
            var model = _mapper.Map<DepartmentEmployee>(departmentEmployee);
            await _context.DepartmentEmployees.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentEmployee>(model);
            return new Response<GetDepartmentEmployee>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetDepartmentEmployee>> UpdateDepartmentEmployee(AddDepartmentEmployee departmentEmployee)
    {
        try
        {
            departmentEmployee.FromDate = DateTime.SpecifyKind(departmentEmployee.FromDate, DateTimeKind.Utc);
            departmentEmployee.ToDate = DateTime.SpecifyKind(departmentEmployee.ToDate, DateTimeKind.Utc);
            var find = await _context.DepartmentEmployees.FindAsync(departmentEmployee.EmployeeId);
            if (find == null) return new Response<GetDepartmentEmployee>(new GetDepartmentEmployee());
            _mapper.Map(departmentEmployee, find);
            _context.Entry(find).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentEmployee>(find);
            return new Response<GetDepartmentEmployee>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<bool>> DeleteDepartmentEmployee(int id)
    {
        try
        {
            var find = await _context.DepartmentEmployees.FindAsync(id);
            if (find != null)
            {
                _context.DepartmentEmployees.Remove(find);
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

    public async Task<Response<GetDepartmentEmployee>> GetDepartmentEmployeeById(int id)
    {
        try
        {
            var find = await _context.DepartmentEmployees.FindAsync(id);
            if (find != null)
            {
                var result = _mapper.Map<GetDepartmentEmployee>(find);
                return new Response<GetDepartmentEmployee>(result);
            }

            return new Response<GetDepartmentEmployee>(new GetDepartmentEmployee());
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<List<GetDepartmentEmployee>>> GetDepartmentEmployees()
    {
        try
        {
            var model = await _context.DepartmentEmployees.ToListAsync();
            var result =_mapper.Map<List<GetDepartmentEmployee>>(model);
            return new Response<List<GetDepartmentEmployee>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetDepartmentEmployee>>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }
}