using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TALPA.Models;
using BLL.Models;
using System.Linq.Expressions;
using BLL;
using Microsoft.IdentityModel.Tokens;

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
            ViewBag.Employee = employeeUtility.GetEmployee(User);

            bool ActivityPlanned = activityManager.ActivityPlanned(ViewBag.Employee.Team);

			ActivityViewModel activityViewModel = new ActivityViewModel
            {
				ActivityPlanned = ActivityPlanned,
			};

            if (ActivityPlanned)
            {
                activityViewModel.Activity = activityManager.GetActivity(ViewBag.Employee.Team);

			}
            return View(activityViewModel);
        }

		[Authorize]
		public IActionResult Suggestions()
        {
            ViewBag.Employee = employeeUtility.GetEmployee(User);

            string  search = Request.Query["search"];
            string sort = Request.Query["sort"];
			string filter = Request.Query["filter"];

			search = string.IsNullOrEmpty(search) ? "" : search;
            sort = string.IsNullOrEmpty(sort) ? "trending" : sort;
			filter = string.IsNullOrEmpty(filter) ? "" : filter;

			List<string> filterList = filter.Split(' ').ToList();

			List<Suggestion> suggestionResults = suggestionManager.GetSuggestions(search, sort, filterList);
			SuggestionsViewModel suggestionsViewModel = new SuggestionsViewModel
			{
				Suggestions = suggestionResults,
				Categories = suggestionManager.GetCategories(),
				Limitations = suggestionManager.GetLimitations(),
				Search = search,
				Sort = sort,
				Filter = filterList,
				Results = suggestionResults.Count,
			};
            return View(suggestionsViewModel);
        }

		[Authorize]
		public IActionResult Poll()
        {
			ViewBag.Employee = employeeUtility.GetEmployee(User);

			PollViewModel pollViewModel = new PollViewModel
            {
                PollActive = pollManager.PollActive(ViewBag.Employee.Team),
                PollChosen = pollManager.PollChosen(ViewBag.Employee.Id),
                Poll = pollManager.GetPoll(ViewBag.Employee.Team)
			};
            return View(pollViewModel);
        }
	}
}
