using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;

namespace TALPA.Controllers
{
	public class AppController : Controller
    {
        [Authorize]
		public IActionResult Uitje()
        {
            ViewBag.Employee = EmployeeUtility.GetEmployee(User);
            return View();
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
            ViewBag.Employee = EmployeeUtility.GetEmployee(User);
            return View();
        }
    }
}
