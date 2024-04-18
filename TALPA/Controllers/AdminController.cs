using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using BLL;

namespace TALPA.Controllers
{
	public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Dashboard()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };

            if (UserProfile.Role != "Admin")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = new List<Employee>();
            for (int i = 1; i <= 10; i++)
            {
                employees.Add(new Employee("Employee" + i, "Employee" + i + "@talpa.com", i, i, i, i));
            }

            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees
            };

            return View(adminDashboardViewModel);
        }
    }
}
