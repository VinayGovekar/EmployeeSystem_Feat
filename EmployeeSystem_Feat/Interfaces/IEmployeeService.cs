using EmployeeSystem_Feat.Models;

namespace EmployeeSystem_Feat.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeViewModel> GetAllEmployees();
    }
}
