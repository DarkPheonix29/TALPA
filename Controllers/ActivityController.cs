using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL;

namespace TALPA.Controllers
{
    public class ActivityController : Controller
    {
        private readonly SuggestionManager suggestionManager;
        private readonly EmployeeManager employeeManager;

        public ActivityController()
        {
            suggestionManager = new SuggestionManager();
            employeeManager = new EmployeeManager();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateSuggestion(object model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return Redirect("/dashboard");
        }
    }
}