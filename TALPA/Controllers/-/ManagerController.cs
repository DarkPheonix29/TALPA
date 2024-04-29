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
	public class EmployeeUtility1 : Controller
    {
        private readonly SuggestionManager suggestionManager;
        private readonly EmployeeManager employeeManager;
        private readonly PollManager pollManager;

        public EmployeeUtility1()
        {
            suggestionManager = new SuggestionManager();
            employeeManager = new EmployeeManager();
            pollManager = new PollManager();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int team = employeeManager.GetUserTeam(user);

            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Team = "team "+team,
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = employeeManager.GetTeamEmployees(team);
            Poll poll = pollManager.getCurrentPoll(team);
            List<string>  suggestions = new List<string>();
            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees,
                Suggestions = suggestions,
                Poll = poll,
            };

            ViewBag.AlertMessage = TempData["AlertMessage"] ?? "";

            return View(managerDashboardViewModel);
        }

        [Authorize]
        public IActionResult Employees()
        {
            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int team = employeeManager.GetUserTeam(user);

            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Team = "team " + team,
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Employee> employees = employeeManager.GetTeamEmployees(team);
            Poll poll = pollManager.getCurrentPoll(team);
            ManagerDashboardViewModel managerDashboardViewModel = new ManagerDashboardViewModel
            {
                UserProfile = UserProfile,
                Employees = employees,
                Poll = poll
            };

            return View(managerDashboardViewModel);
        }

        public IActionResult CreatePoll()
        {
            string user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int team = employeeManager.GetUserTeam(user);

            var UserProfile = new UserProfile
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "https://localhost:7112/username")?.Value,
                EmailAddress = User.Identity.Name,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
                Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                Team = "team " + team,
                Points = 0,
            };

            if (UserProfile.Role != "Manager")
            {
                return Redirect("/dashboard");
            }

            List<Suggestion> suggestions = suggestionManager.GetSuggestions();

            ActivityViewModel activityViewModel = new ActivityViewModel
            {

            };

            return View(activityViewModel);
        }
    }
}
