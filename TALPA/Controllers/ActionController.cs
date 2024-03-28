using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;

namespace TALPA.Controllers
{
    public class ActionController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSuggestion(SuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                string restrictionString = "";
                foreach (var restriction in model.Restrictions)
                {
                    restrictionString += $"{restriction}, ";
                }
                return Content($"Activity: {model.Activity} <br> Restrictions: {restrictionString}");
            }
            return Content("error");
        }
    }
}
