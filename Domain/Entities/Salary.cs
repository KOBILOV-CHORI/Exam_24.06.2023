using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Salary
{
    [Key]
    public Int64 Id { get; set; }
    [ForeignKey("Employee")]
    public Int64 EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public Int64 Amount { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}