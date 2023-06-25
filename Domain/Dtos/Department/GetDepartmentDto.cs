namespace Domain.Dtos.Department;

public class GetDepartmentDto : DepartmentBaseDto
{
    public Int64 ManagerId { get; set; }
    public string? ManagerFullName { get; set; }
}