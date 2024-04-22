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
	public class EmployeeController : Controller
    {
        private readonly SuggestionManager suggestionManager;

        public EmployeeController()
        {
            suggestionManager = new SuggestionManager();
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

            if (UserProfile.Role != "Medewerker")
            {
                return Redirect("/dashboard");
            }

            string user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            List<Suggestion> suggestions = suggestionManager.GetUserSuggestions(user);

            EmployeeDashboardViewModel employeeViewModel = new EmployeeDashboardViewModel
            {
                UserProfile = UserProfile,
                Suggestions = suggestions
            };

            return View(employeeViewModel);
        }
    }
}
