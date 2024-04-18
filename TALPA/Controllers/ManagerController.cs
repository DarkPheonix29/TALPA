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
	public class ManagerController : Controller
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

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = new List<Employee>();
            for (int i = 1; i <= 10; i++)
            {
                employees.Add(new Employee("Employee" + i, "Employee" + i + "@talpa.com", i, i, i, i));
            }

            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees
            };

            return View(managerDashboardViewModel);
        }

        [Authorize]
        public IActionResult Employees()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = new List<Employee>();
            for (int i = 1; i <= 10; i++)
            {
                employees.Add(new Employee("Employee" + i, "Employee" + i + "@talpa.com", i, i, i, i));
            }

            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees
            };

            return View(managerDashboardViewModel);
        }

        public IActionResult CreatePoll()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<BLL.Models.Activity> activities = new List<BLL.Models.Activity>();
            for (int i = 1; i <= 10; i++)
            {
                List<string> categories = new List<string>();
                List<Restriction> restrictions = new List<Restriction>();
                string description = string.Concat(Enumerable.Repeat("Description", 7));
                for (int j = 1; j <= 3; j++)
                {
                    categories.Add("Cata" + i);
                    restrictions.Add(new Restriction("restriction" + i, "description", "cata"));
                }
                activities.Add(new BLL.Models.Activity(i, "Activiteit" + i, description, categories, restrictions));
            }

            ActivityViewModel activityViewModel = new ActivityViewModel
            {
                UserProfile = UserProfile,
                Activities = activities
            };

            return View(activityViewModel);
        }
    }
}
