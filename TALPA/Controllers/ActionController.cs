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
                return Content($"Activity: {model.Activity} | Restrictions: {restrictionString}");
            }
            return Content("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeChoice(ChoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                string availableDatesString = "";
                foreach (var availableDate in model.AvailableDates)
                {
                    availableDatesString += $"{availableDate}, ";
                }
                return Content($"Activity: {model.Activity} | Available Date: {availableDatesString}");
            }
            return Content("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeAvailability(ChangeAvailabilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                string availabilityString = "";
                foreach (var availableDate in model.AvailableDates)
                {
                    availabilityString += $"{availableDate}, ";
                }
                return Content($"New Availability: {availabilityString}");
            }
            return Content("error");
        }
    }
}