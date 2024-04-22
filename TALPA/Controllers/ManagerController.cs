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
        private readonly SuggestionManager suggestionManager;
        private readonly EmployeeManager employeeManager;

        public ManagerController()
        {
            suggestionManager = new SuggestionManager();
            employeeManager = new EmployeeManager();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Team = "team",
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int team = employeeManager.GetUserTeam(user);

            List<Employee> employees = employeeManager.GetTeamEmployees(team);

            List<string>  suggestions = new List<string>();
            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees,
                Suggestions = suggestions
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
                Team = "team",
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int team = employeeManager.GetUserTeam(user);

            List<Employee> employees = employeeManager.GetTeamEmployees(team);

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
                Team = "team",
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Suggestion> suggestions = suggestionManager.GetSuggestions();

            ActivityViewModel activityViewModel = new ActivityViewModel
            {
                UserProfile = UserProfile,
                Suggestions = suggestions
            };

            return View(activityViewModel);
        }
    }
}
