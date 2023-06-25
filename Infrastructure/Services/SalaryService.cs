using System.Net;
using AutoMapper;
using Domain.Dtos.Employee;
using Domain.Dtos.Salary;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SalaryService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SalaryService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<GetSalary>> AddSalary(AddSalary salary)
    {
        try
        {
            salary.FromDate = DateTime.SpecifyKind(salary.FromDate, DateTimeKind.Utc);
            salary.ToDate = DateTime.SpecifyKind(salary.ToDate, DateTimeKind.Utc);
            var model = _mapper.Map<Salary>(salary);
            await _context.Salaries.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetSalary>(model);
            return new Response<GetSalary>(result);
        }
        catch (Exception e)
        {
            return new Response<GetSalary>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<GetSalary>> UpdateSalary(AddSalary salary)
    {
        try{
        salary.FromDate = DateTime.SpecifyKind(salary.FromDate, DateTimeKind.Utc);
        salary.ToDate = DateTime.SpecifyKind(salary.ToDate, DateTimeKind.Utc);
        var find = await _context.Salaries.FindAsync(salary.Id);
        if (find == null) return new Response<GetSalary>(new GetSalary());
        _mapper.Map(salary, find);
        _context.Entry(find).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        var result = _mapper.Map<GetSalary>(find);
        return new Response<GetSalary>(result);
        }
        catch (Exception e)
        {
            return new Response<GetSalary>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }

    public async Task<Response<bool>> DeleteSalary(int id)
    {
        try
        {
            var find = await _context.Salaries.FindAsync(id);
            if (find != null)
            {
                _context.Salaries.Remove(find);
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

    public async Task<Response<GetSalary>> GetSalaryById(int id)
    {
        try
        {
            var find = await _context.Salaries.FindAsync(id);
            if (find != null)
            {
                var result = _mapper.Map<GetSalary>(find);
                return new Response<GetSalary>(result);
            }

            return new Response<GetSalary>(new GetSalary());
        }
        catch (Exception e)
        {
            return new Response<GetSalary>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
        
    }

    public async Task<Response<List<GetSalary>>> GetSalaries()
    {
        try
        {
            var model = await _context.Salaries.ToListAsync();
            var result = _mapper.Map<List<GetSalary>>(model);
            return new Response<List<GetSalary>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetSalary>>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
        }
    }
}