

using BLL.Models;
namespace TALPA.Models
{
    public class ManagerDashboardViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Employee> Employees { get; set; }
        public List<string> Suggestions { get; set; }
        public Poll Poll { get; set; }
    }
}
