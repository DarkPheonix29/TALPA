using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;

namespace TALPA.Controllers
{
    public class HomeController() : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/uitje");
            }
            return View();
        }
    }
}