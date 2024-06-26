﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TALPA.Models;
using BLL.Models;
using BLL;
using System.Text.RegularExpressions;
using TALPA_ai_test;

namespace TALPA.Controllers
{
	public class AppController : Controller
    {
		private readonly EmployeeUtility employeeUtility;
		private readonly ActivityManager activityManager;
		private readonly SuggestionManager suggestionManager;
		private readonly PollManager pollManager;
		private readonly AiManager aiManager;

		public AppController()
		{
			employeeUtility = new EmployeeUtility();
			activityManager = new ActivityManager();
			suggestionManager = new SuggestionManager(new Logger());
			pollManager = new PollManager();
			aiManager = new AiManager();
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

            if (activityViewModel.ActivityPlanned)
            {
	            ActivityManager am = new();

	            if (am.RemoveActivityAfterDeadline(am.GetPlannedActivityIdByTeamName(employee.Team),
		                Convert.ToDateTime(activityViewModel.Activity.StartDate)))
	            {
		            return Redirect("/uitje");
				}
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
                if (pollManager.EndPoll(employee.Team))
                {
	                return Redirect("/stemmen");
                }

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
				name.Length >= 3 &&
				name.Length <= 30 &&
				description.Length >= 5 &&
				description.Length <= 150
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
				pollManager.SubmitPoll(employee.Id, suggestionInt, availability, pollManager.GetPollIdWithTeamName(employee.Team));
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
				bool created = pollManager.CreatePoll( employee.Team, activitiesInt, date, availability);
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

        [HttpPost]
        [Authorize]
		public async Task<IActionResult> GetSimilarSuggestions(string name, string description)
		{
			if (
				!string.IsNullOrWhiteSpace(name) &&
				!string.IsNullOrWhiteSpace(description)
			)
			{
				List<string> SqlInjections = new List<string> { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "UNION", "WHERE", "AND", "OR", "LIKE", "EXEC", "EXECUTE", "TRUNCATE", "ORDER BY", "GROUP BY", "--", ";", "/*", "*/", "XP_CMDShell" };
				if (SqlInjections.Any(keyword => name.Contains(keyword) || description.Contains(keyword)))
				{
					return Content("Nice Try! Ik ga je kietelen.");
				}

				Employee employee = employeeUtility.GetEmployee(User);
				List<Suggestion> suggestions = suggestionManager.GetSuggestions(employee.Id, "", "popular", new List<string>(), new List<int>());
				Suggestion suggestion = new Suggestion
				{
					Name = name,
					Description = description
				};

				List<int> similars = await aiManager.GetSimilars(suggestions, suggestion);
				return Json(similars);
			}
			return Content("Invalid");
		}

		[Authorize]
		[Route("removeSuggestion/{suggestion:int}")]
		public IActionResult RemoveSuggestion(int suggestion)
		{
			Employee employee = employeeUtility.GetEmployee(User);
			if (employee.Role == "Admin")
			{
				suggestionManager.RemoveSuggestion(suggestion);
				TempData["message"] = "Suggestie is verwijderd!";
				return Redirect("/suggesties");
			}
			else
			{
				TempData["errorMessage"] = "Je hebt geen toegang!";
				return Redirect("/suggesties");
			}
		}
	}
}
