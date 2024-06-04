using Microsoft.AspNetCore.Mvc;

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