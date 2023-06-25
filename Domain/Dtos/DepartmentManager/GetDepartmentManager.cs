namespace Domain.Dtos.DepartmentManager;

public class GetDepartmentManager : DepartmentManagerBase
{
    public string ManagerFullName { get; set; }
    public string DepartmentName { get; set; }
    public DateTime ToDate { get; set; }
}