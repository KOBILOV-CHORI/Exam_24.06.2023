namespace Domain.Dtos.DepartmentEmployee;

public class DepartmentEmployeeBase
{
    public Int64 EmployeeId { get; set; }
    public Int64 DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}