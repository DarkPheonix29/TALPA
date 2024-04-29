using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using System.Linq.Expressions;

namespace TALPA.Controllers
{
	public class AppController : Controller
    {
        [Authorize]
		public IActionResult Uitje()
        {
            ViewBag.Employee = EmployeeUtility.GetEmployee(User);
            ActivityViewModel activityViewModel = new ActivityViewModel
            {
				ActivityPlanned = true,
                ActivityName = "Stadswandeling",
                ActivityDescription = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
                ActivityCategories = new List<string> { "Buiten", "Middag" },
                ActivityLimitation = new List<string> { "Tijd", "Alcohol" },
                ActivityLocation = "Molendijk 6, 6107 AA Stevensweert",
                ActivityStartDate = "10-04-2024 17:00",
                ActivityEndDate = "10-04-2024 19:30"
			};
            return View(activityViewModel);
        }

		[Authorize]
		public IActionResult Suggesties()
        {
            ViewBag.Employee = EmployeeUtility.GetEmployee(User);
            return View();
        }

		[Authorize]
		public IActionResult Stemmen()
        {
            PollViewModel pollViewModel = new PollViewModel
            {
                PollActive = true,
                PollChosen = true,
            };
            ViewBag.Employee = EmployeeUtility.GetEmployee(User);
            return View(pollViewModel);
        }
    }
}
