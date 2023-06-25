using System.Net;
using AutoMapper;
using Domain.Dtos.Employee;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public EmployeeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GetEmployee>> AddEmployee(AddEmployee employee)
    {
        try
        {
            employee.BirthDate = DateTime.SpecifyKind(employee.BirthDate, DateTimeKind.Utc);
            employee.HireDate = DateTime.SpecifyKind(employee.HireDate, DateTimeKind.Utc);
            var model = _mapper.Map<Employee>(employee);
            await _context.Employees.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetEmployee>(model);
            return new Response<GetEmployee>(result);
        }
        catch (Exception e)
        {
            return new Response<GetEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetEmployee>> UpdateEmployee(AddEmployee employee)
    {
        try
        {
            employee.BirthDate = DateTime.SpecifyKind(employee.BirthDate, DateTimeKind.Utc);
            employee.HireDate = DateTime.SpecifyKind(employee.HireDate, DateTimeKind.Utc);
            var find = await _context.Employees.FindAsync(employee.Id);
            _mapper.Map(employee, find);
            _context.Entry(find).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetEmployee>(find);
            return new Response<GetEmployee>(result);
        }
        catch (Exception e)
        {
            return new Response<GetEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<bool>> DeleteEmployee(int id)
    {
        try
        {
            var find = await _context.Employees.FindAsync(id);
            if (find != null)
            {
                _context.Employees.Remove(find);
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

    public async Task<Response<GetEmployee>> GetEmployeeById(int id)
    {
        try
        {
            var result = await (
                from e in _context.Employees
                from de in e.DepartmentEmployees
                where e.Id == id
                select new GetEmployee()
                {
                    Id = e.Id,
                    DepartmentId = de.Department.Id,
                    DepartmentName = de.Department.Name,
                    FullName = $"{e.FirstName} {e.LastName}"
                }).FirstOrDefaultAsync();
            return new Response<GetEmployee>(result);
        }
        catch (Exception e)
        {
            return new Response<GetEmployee>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<List<GetEmployee>>> GetEmployees()
    {
        try
        {
            var result = await (
                from e in _context.Employees
                from de in e.DepartmentEmployees
                select new GetEmployee()
                {
                    Id = e.Id,
                    DepartmentId = de.Department.Id,
                    DepartmentName = de.Department.Name,
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToListAsync();
            return new Response<List<GetEmployee>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetEmployee>>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }
}