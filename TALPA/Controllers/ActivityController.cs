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
        private readonly SuggestionManager suggestionManager;
        private readonly PollManager pollManager;
        private readonly EmployeeManager employeeManager;

        public ActivityController()
        {
            suggestionManager = new SuggestionManager();
            pollManager = new PollManager();
            employeeManager = new EmployeeManager();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CreateSuggestion(SuggestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                string user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string activity = model.Activity;
                string description = model.Description;
                List<string> limitations = model.Limitations;
                List<string> categories = model.Categories;

                suggestionManager.SubmitSuggestion(user, activity, description, limitations, categories);
            }
            return Redirect("/dashboard");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeDeadline(ChangeDeadlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Content($"New Deadline: {model.DeadlineDate}");
            }
            return Content("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePoll(CreatePollViewModel model)
        {
            if (ModelState.IsValid)
            {
                string user = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                int team = employeeManager.GetUserTeam(user);
                pollManager.CreatePoll(team, model.Dates, model.Activities.Select(s => int.Parse(s)).ToList());
       
                TempData["AlertMessage"] = "Stemming is aangemaakt.";
                return Redirect($"/dashboard");
            }
            return Content("error");
        }
    }
}