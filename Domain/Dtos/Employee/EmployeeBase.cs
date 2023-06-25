namespace Domain.Dtos.Employee;

public class EmployeeBase
{
    public Int64 Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
}