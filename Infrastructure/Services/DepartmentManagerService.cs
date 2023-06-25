using System.Net;
using AutoMapper;
using Domain.Dtos.DepartmentManager;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DepartmentManagerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public DepartmentManagerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GetDepartmentManager>> AddDepartmentManager(AddDepartmentManager departmentManager)
    {
        try
        {
            departmentManager.FromDate = DateTime.SpecifyKind(departmentManager.FromDate, DateTimeKind.Utc);
            var model = _mapper.Map<DepartmentManager>(departmentManager);
            await _context.DepartmentManagers.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentManager>(model);
            return new Response<GetDepartmentManager>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentManager>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetDepartmentManager>> UpdateDepartmentManager(AddDepartmentManager departmentManager)
    {
        try
        {
            departmentManager.FromDate = DateTime.SpecifyKind(departmentManager.FromDate, DateTimeKind.Utc);
            var find = await _context.DepartmentManagers.FindAsync(departmentManager.EmployeeId);
            if (find == null) return new Response<GetDepartmentManager>(new GetDepartmentManager());
            _mapper.Map(departmentManager, find);
            _context.Entry(find).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetDepartmentManager>(find);
            return new Response<GetDepartmentManager>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentManager>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<bool>> DeleteDepartmentManager(int id)
    {
        try
        {
            var find = await _context.DepartmentManagers.FindAsync(id);
            if (find != null)
            {
                _context.DepartmentManagers.Remove(find);
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

    public async Task<Response<GetDepartmentManager>> GetDepartmentManagerById(int id)
    {
        try
        {
            var result = await (
                from dm in _context.DepartmentManagers
                where dm.EmployeeId == id
                select new GetDepartmentManager()
                {
                    EmployeeId = dm.EmployeeId,
                    FromDate = dm.FromDate,
                    DepartmentId = dm.DepartmentId,
                    ToDate = dm.ToDate,
                    DepartmentName = dm.Department.Name,
                    ManagerFullName = $"{dm.Employee.FirstName} {dm.Employee.LastName}"
                }).FirstOrDefaultAsync();
            return new Response<GetDepartmentManager>(result);
        }
        catch (Exception e)
        {
            return new Response<GetDepartmentManager>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<List<GetDepartmentManager>>> GetDepartmentManagers()
    {
        try
        {
            var result = await (
                from dm in _context.DepartmentManagers
                select new GetDepartmentManager()
                {
                    EmployeeId = dm.EmployeeId,
                    FromDate = dm.FromDate,
                    DepartmentId = dm.DepartmentId,
                    ToDate = dm.ToDate,
                    DepartmentName = dm.Department.Name,
                    ManagerFullName = $"{dm.Employee.FirstName} {dm.Employee.LastName}"
                }).ToListAsync();
            return new Response<List<GetDepartmentManager>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetDepartmentManager>>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }
}