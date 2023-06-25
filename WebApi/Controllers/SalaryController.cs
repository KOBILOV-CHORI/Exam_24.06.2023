using Domain.Dtos.Salary;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryController : ControllerBase
{
    private readonly SalaryService _salaryService;

    public SalaryController(SalaryService salaryService)
    {
        _salaryService = salaryService;
    }
    [HttpPost("AddSalary")]
    public async Task<Response<GetSalary>> AddSalary(AddSalary employee)
    {
        return await _salaryService.AddSalary(employee);
    }

    [HttpPut("UpdateSalary")]
    public async Task<Response<GetSalary>> UpdateSalary(AddSalary employee)
    {
        return await _salaryService.UpdateSalary(employee);
    }

    [HttpDelete("DeleteSalary")]
    public async Task<Response<bool>> DeleteSalary(int id)
    {
        return await _salaryService.DeleteSalary(id);
    }

    [HttpGet("GetSalaryById")]
    public async Task<Response<GetSalary>> GetSalaryById(int id)
    {
        return await _salaryService.GetSalaryById(id);
    }

    [HttpGet("GetSalaries")]
    public async Task<Response<List<GetSalary>>> GetSalaries()
    {
        return await _salaryService.GetSalaries();
    }
}