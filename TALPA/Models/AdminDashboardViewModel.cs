

using BLL.Models;
namespace TALPA.Models
{
    public class AdminDashboardViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
