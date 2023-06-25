using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class DepartmentEmployee
{
    [Key]
    [ForeignKey("Employee")]
    public Int64 EmployeeId { get; set; }
    public Employee Employee { get; set; }
    [ForeignKey("Department")]
    public Int64 DepartmentId { get; set; }
    public Department Department { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}