using Domain.Dtos.Department;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    [HttpPost("AddDepartment")]
    public async Task<Response<GetDepartmentDto>> AddDepartment(AddDepartmentDto department)
    {
        return await _departmentService.AddDepartment(department);
    }

    [HttpPut("UpdateDepartment")]
    public async Task<Response<GetDepartmentDto>> UpdateddDepartment(AddDepartmentDto department)
    {
        return await _departmentService.UpdateDepartment(department);
    }

    [HttpDelete("DeleteddDepartment")]
    public async Task<Response<bool>> DeleteddDepartment(int id)
    {
        return await _departmentService.DeleteDepartment(id);
    }

    [HttpGet("GetDepartmentByIdDto")]
    public async Task<Response<GetDepartmentDto>> GetDepartmentByIdDto(int id)
    {
        return await _departmentService.GetDepartmentById(id);
    }

    [HttpGet("GetDepartmentsDto")]
    public async Task<Response<List<GetDepartmentDto>>> GetDepartmentsDto()
    {
        return await _departmentService.GetDepartments();
    }
}