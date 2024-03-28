using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL;

namespace TALPA.Controllers
{
    public class ActionController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateSuggestion(SuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<LimitationTypes> limitationList = new();
                foreach (var limitation in model.limitations)
                {
                    Enum.TryParse("Active", out LimitationTypes limitatio);
                    limitationList.Add(limitatio);
                }
                
                ActivityManager am = new();
                User user = new();
                Activity activity = new(model.Activity, "", limitationList, user, DateTime.Now);
                am.SubmitToDatabase(activity);
                return Content("succes");
            }
            return Content("error");
        }
    }
}
