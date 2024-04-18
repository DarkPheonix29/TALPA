

using BLL.Models;
namespace TALPA.Models
{
    public class EmployeeDashboardViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
