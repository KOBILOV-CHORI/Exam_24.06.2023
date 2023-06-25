using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    [Key]
    public Int64 Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    public int Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public List<Salary> Salaries { get; set; }
    public List<DepartmentManager> DepartmentManagers { get; set; }
    public List<DepartmentEmployee> DepartmentEmployees { get; set; }
}