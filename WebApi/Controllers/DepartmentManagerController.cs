using Domain.Dtos.DepartmentManager;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentManagerController : ControllerBase
{
    private readonly DepartmentManagerService _departmentManagerService;

    public DepartmentManagerController(DepartmentManagerService departmentManagerService)
    {
        _departmentManagerService = departmentManagerService;
    }
    [HttpPost("AddDepartmentManager")]
    public async Task<Response<GetDepartmentManager>> AddDepartmentManager(AddDepartmentManager employee)
    {
        return await _departmentManagerService.AddDepartmentManager(employee);
    }

    [HttpPut("UpdateDepartmentManager")]
    public async Task<Response<GetDepartmentManager>> UpdateDepartmentManager(AddDepartmentManager employee)
    {
        return await _departmentManagerService.UpdateDepartmentManager(employee);
    }

    [HttpDelete("DeleteDepartmentManager")]
    public async Task<Response<bool>> DeleteDepartmentManager(int id)
    {
        return await _departmentManagerService.DeleteDepartmentManager(id);
    }

    [HttpGet("GetDepartmentManagerById")]
    public async Task<Response<GetDepartmentManager>> GetDepartmentManagerById(int id)
    {
        return await _departmentManagerService.GetDepartmentManagerById(id);
    }

    [HttpGet("GetDepartmentManagers")]
    public async Task<Response<List<GetDepartmentManager>>> GetDepartmentManagers()
    {
        return await _departmentManagerService.GetDepartmentManagers();
    }
}