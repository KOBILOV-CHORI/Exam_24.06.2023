using Domain.Dtos.Employee;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("AddEmployee")]
    public async Task<Response<GetEmployee>> AddEmployee(AddEmployee employee)
    {
        return await _employeeService.AddEmployee(employee);
    }

    [HttpPut("UpdateEmployee")]
    public async Task<Response<GetEmployee>> UpdateEmployee(AddEmployee employee)
    {
        return await _employeeService.UpdateEmployee(employee);
    }

    [HttpDelete("DeleteEmployee")]
    public async Task<Response<bool>> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }

    [HttpGet("GetEmployeeById")]
    public async Task<Response<GetEmployee>> GetEmployeeById(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }

    [HttpGet("GetEmployees")]
    public async Task<Response<List<GetEmployee>>> GetEmployees()
    {
        return await _employeeService.GetEmployees();
    }
}