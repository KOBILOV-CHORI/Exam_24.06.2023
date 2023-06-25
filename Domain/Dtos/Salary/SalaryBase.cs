namespace Domain.Dtos.Salary;

public class SalaryBase
{
    public Int64 Id { get; set; }
    public Int64 EmployeeId { get; set; }
    public Int64 Amount { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}