using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL;
using Microsoft.AspNetCore.SignalR;

namespace TALPA.Controllers
{
    public class ActivityController : Controller
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
                Activity activity = new(model.Activity, "", limitationList, User, DateTime.Now);
                am.SubmitToDatabase(activity);
                return Content("succes");
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
