using Domain.Dtos.DepartmentEmployee;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentEmployeeController : ControllerBase
{
    private readonly DepartmentEmployeeService _departmentEmployeeService;

    public DepartmentEmployeeController(DepartmentEmployeeService departmentEmployeeService)
    {
        _departmentEmployeeService = departmentEmployeeService;
    }
    [HttpPost("AddDepartmentEmployee")]
    public async Task<Response<GetDepartmentEmployee>> AddDepartmentEmployee(AddDepartmentEmployee departmentEmployee)
    {
        return await _departmentEmployeeService.AddDepartmentEmployee(departmentEmployee);
    }

    [HttpPut("UpdateDepartmentEmployee")]
    public async Task<Response<GetDepartmentEmployee>> UpdateDepartmentEmployee(AddDepartmentEmployee departmentEmployee)
    {
        return await _departmentEmployeeService.UpdateDepartmentEmployee(departmentEmployee);
    }

    [HttpDelete("DeleteDepartmentEmployee")]
    public async Task<Response<bool>> DeleteDepartmentEmployee(int id)
    {
        return await _departmentEmployeeService.DeleteDepartmentEmployee(id);
    }

    [HttpGet("GetDepartmentEmployeeById")]
    public async Task<Response<GetDepartmentEmployee>> GetDepartmentEmployeeById(int id)
    {
        return await _departmentEmployeeService.GetDepartmentEmployeeById(id);
    }

    [HttpGet("GetDepartmentEmployees")]
    public async Task<Response<List<GetDepartmentEmployee>>> GetDepartmentEmployees()
    {
        return await _departmentEmployeeService.GetDepartmentEmployees();
    }
}