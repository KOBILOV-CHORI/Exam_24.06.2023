using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Department
{
    [Key]
    public Int64 Id { get; set; }
    [MaxLength(100)] 
    public string Name { get; set; }
    public List<DepartmentManager> DepartmentManagers { get; set; }
    public List<DepartmentEmployee> DepartmentEmployees { get; set; }
}