using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using System.Linq.Expressions;
using BLL;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TALPA.Controllers
{
	public class AppController : Controller
    {
		private readonly EmployeeUtility employeeUtility;
		private readonly ActivityManager activityManager;
		private readonly SuggestionManager suggestionManager;
		private readonly PollManager pollManager;

		public AppController()
		{
			employeeUtility = new EmployeeUtility();
			activityManager = new ActivityManager();
			suggestionManager = new SuggestionManager();
			pollManager = new PollManager();
		}

		[Authorize]
		public IActionResult Activity()
        {
			Employee employee = employeeUtility.GetEmployee(User);
			ViewBag.Employee = employee;


			bool ActivityPlanned = activityManager.ActivityPlanned(employee.Team);

			ActivityViewModel activityViewModel = new ActivityViewModel
            {
				ActivityPlanned = ActivityPlanned,
			};

            if (ActivityPlanned)
            {
                activityViewModel.Activity = activityManager.GetActivity(employee.Team);

			}
            return View(activityViewModel);
        }

		[Authorize]
		public IActionResult Suggestions()
        {
			Employee employee = employeeUtility.GetEmployee(User);
			ViewBag.Employee = employee;
			ViewBag.message = TempData["message"] ?? "";
            ViewBag.errorMessage = TempData["errorMessage"] ?? "";

            string  search = Request.Query["search"];
            string sort = Request.Query["sort"];
			string filter = Request.Query["filter"];
			string selected = Request.Query["selected"];
			string selectedIds = Request.Query["ids"];
			string closed = Request.Query["closed"];

			search = string.IsNullOrEmpty(search) ? "" : search;
            sort = string.IsNullOrEmpty(sort) ? "trending" : sort;
			filter = string.IsNullOrEmpty(filter) ? "" : filter;
			selected = string.IsNullOrEmpty(selected) ? "" : selected;
			selectedIds = string.IsNullOrEmpty(selectedIds) ? "" : selectedIds;
			closed = string.IsNullOrEmpty(closed) ? "" : closed;

			List<string> filterList = filter.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
			List<string> closedList = closed.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
			List<string> selectedList = selected.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Replace("-", " ")).ToList();
			List<int> selectedIdsList = selectedIds.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

			List<Suggestion> suggestionResults = suggestionManager.GetSuggestions(employee.Id, search, sort, filterList, selectedIdsList);
			SuggestionsViewModel suggestionsViewModel = new SuggestionsViewModel
			{
				Suggestions = suggestionResults,
				Categories = suggestionManager.GetCategories(),
				Limitations = suggestionManager.GetLimitations(),
				Search = search,
				Sort = sort,
				Filter = filterList,
				Selected = selectedList,
				SelectedIds =  selectedIdsList,
				Results = suggestionResults.Count,
				Closed = closedList
			};
            return View(suggestionsViewModel);
        }

		[Authorize]
        public IActionResult Poll()
        {
            Employee employee = employeeUtility.GetEmployee(User);
            ViewBag.Employee = employee;
            ViewBag.message = TempData["message"] ?? "";
            ViewBag.errorMessage = TempData["errorMessage"] ?? "";
            bool pollactive = pollManager.PollActive(employee.Team);

            if (pollactive)
            {
                PollViewModel pollViewModel = new PollViewModel
                {
                    PollActive = pollactive,
                    PollChosen = pollManager.PollChosen(employee.Id),
                    Poll = pollManager.GetPoll(employee.Id, employee.Team)
                };

                // Call EndPoll to finalize the poll if the deadline has passed
                pollManager.EndPoll(employee.Team);

                return View(pollViewModel);
            }
            else
            {
                PollViewModel pollViewModel = new PollViewModel
                {
                    PollActive = pollactive,
                };
                return View(pollViewModel);
            }
        }

        [Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SubmitSuggestion(string name, string description, List<string> categories, List<string> limitations)
		{
			if (
				!string.IsNullOrWhiteSpace(name) && 
				!string.IsNullOrWhiteSpace(description) &&
				categories.Any() &&
				limitations.Any() &&
				name.Length >= 5 &&
				name.Length <= 30 &&
				description.Length >= 30 &&
				description.Length <= 150 &&
				categories.Count >= 1 &&
				limitations.Count >= 1
			)
			{
				Employee employee = employeeUtility.GetEmployee(User);
				suggestionManager.SubmitSuggestion(employee.Id, name, description, categories, limitations);
				TempData["message"] = "We hebben je suggestie ontvangen!";
				return Redirect("/suggesties");
			}
			return Content("Invalid");
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SubmitPoll(string suggestion, List<string> availability)
		{
			if (
				!string.IsNullOrWhiteSpace(suggestion) &&
				availability.Any()
			)
			{
				Employee employee = employeeUtility.GetEmployee(User);
				int suggestionInt = Convert.ToInt32(suggestion);
				pollManager.SubmitPoll(employee.Id, suggestionInt, availability);
				TempData["message"] = "We hebben je Keuze ontvangen!";
				return Redirect("/stemmen");
			}
			return Content("Invalid");
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreatePoll(List<string> activities, List<string> availability, string deadline, string time)
		{
			if (
				!string.IsNullOrWhiteSpace(deadline) &&
				!string.IsNullOrWhiteSpace(time) &&
				activities.Count >= 3 &&
				availability.Count >= 1
			)
			{
				Employee employee = employeeUtility.GetEmployee(User);
				List<int> activitiesInt = activities.Select(activity => int.Parse(activity)).ToList();
				activitiesInt = activitiesInt.Take(3).ToList();
				string date = deadline + " " + time.Replace(" ", "");
				date = Regex.Replace(date, @"\s+", " ");
				bool created = pollManager.CreatePoll( employee.Team, activitiesInt, date);
				if (created)
				{
					TempData["message"] = "Stemming is aangemaakt!";
                    return Redirect("/stemmen");
                }
				else
				{
					TempData["errorMessage"] = "Er is al een stemming bezig!";
                    return Redirect("/suggesties");
                }
			}
			return Content("Invalid");
		}
	}
}
