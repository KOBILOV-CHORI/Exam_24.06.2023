namespace Domain.Dtos.Employee;

public class GetEmployee
{
    public Int64 Id { get; set; }
    public string FullName { get; set; }
    public Int64 DepartmentId { get; set; }
    public string DepartmentName { get; set; }
}