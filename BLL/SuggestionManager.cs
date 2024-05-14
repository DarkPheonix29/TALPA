using System.Data;
using BLL.Models;
using DAL;

namespace BLL
{
    public class SuggestionManager
    {
        public List<Suggestion> GetSuggestions(string user, string search, string sort, List<string> filter, List<int> selected)
        {
	        ActivityDataManager adm = new();
			List<Suggestion> suggestions = new();
			if (filter.Contains("Mijn-suggesties"))
			{
				DataTable dt = adm.GetActivityFromUser(user);
				foreach (DataRow row in dt.Rows)
				{
					if (!Convert.ToBoolean(row["has_been_chosen"]))
					{
						List<string> votedUsers = adm.GetVotedUsers(Convert.ToInt32(row["id"]));

						DataTable limitationsData = adm.GetLimitations(Convert.ToInt32(row["id"]));
						List<string> limitations = new();
						DataTable categoriesData = adm.GetCategories(Convert.ToInt32(row["id"]));
						List<string> categories = new();
						foreach (DataRow lrow in limitationsData.Rows)
						{
							limitations.Add(lrow["limitation"].ToString());
						}

						foreach (DataRow crow in categoriesData.Rows)
						{
							categories.Add(crow["category"].ToString());
						}

						Suggestion suggestion = new Suggestion
						{
							Id = Convert.ToInt32(row["id"]),
							Name = row["name"].ToString(),
							Description = row["description"].ToString(),
							Categories = categories,
							Limitations = limitations,
							Date = row["date_added"].ToString(),
							Votes = votedUsers.Count
						};

						suggestions.Add(suggestion);
					}
				}
			} 
			else
			{
				DataTable dt = adm.GetAllActivity();
				foreach (DataRow row in dt.Rows)
				{
					if (!Convert.ToBoolean(row["has_been_chosen"]))
					{
						List<string> votedUsers = adm.GetVotedUsers(Convert.ToInt32(row["id"]));

						DataTable limitationsData = adm.GetLimitations(Convert.ToInt32(row["id"]));
						List<string> limitations = new();
						DataTable categoriesData = adm.GetCategories(Convert.ToInt32(row["id"]));
						List<string> categories = new();
						foreach (DataRow lrow in limitationsData.Rows)
						{
							limitations.Add(lrow["limitation"].ToString());
						}

						foreach (DataRow crow in categoriesData.Rows)
						{
							categories.Add(crow["category"].ToString());
						}

						Suggestion suggestion = new Suggestion
						{
							Id = Convert.ToInt32(row["id"]),
							Name = row["name"].ToString(),
							Description = row["description"].ToString(),
							Categories = categories,
							Limitations = limitations,
							Date = row["date_added"].ToString(),
							Votes = votedUsers.Count
						};

						suggestions.Add(suggestion);
					}
				}
			}

			List<Suggestion> selectedSuggestions = suggestions.Where(suggestion => selected.Contains(suggestion.Id)).ToList();
			suggestions.RemoveAll(suggestion => selected.Contains(suggestion.Id));

			suggestions = SearchSuggestions(search, suggestions);
			suggestions = FilterSuggestions(filter, suggestions);
			suggestions = SortSuggestions(sort, suggestions);

			suggestions.AddRange(selectedSuggestions);
			suggestions = suggestions.OrderBy(suggestion => selected.Contains(suggestion.Id) ? 0 : 1).ToList();

			return suggestions;
        }

		public List<string> GetCategories()
		{
			ActivityDataManager adm = new();
			DataTable categoriesData = adm.GetAllUniqueCategories();
			List<string> categories = new();

			foreach (DataRow row in categoriesData.Rows)
			{
				categories.Add(row["category"].ToString());
			}

			return categories;
		}

		public List<string> GetLimitations()
		{
			ActivityDataManager adm = new();
			DataTable limitationData = adm.GetAllUniqueLimitations();
			List<string> limitations = new();

			foreach (DataRow row in limitationData.Rows)
			{
				limitations.Add(row["limitation"].ToString());
			}

			return limitations;
		}

		private List<Suggestion> SearchSuggestions(string search, List<Suggestion> suggestions)
		{
			suggestions = suggestions.Where(suggestion =>
				suggestion.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
				suggestion.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
			).ToList();
			return suggestions;
		}

		private List<Suggestion> FilterSuggestions(List<string> filters, List<Suggestion> suggestions)
		{
			if (filters.Count > 0 && !(filters.Count == 1 && filters.Contains("Mijn-suggesties")))
			{
				List<Suggestion> filtered = new List<Suggestion>();
				foreach(Suggestion suggestion in suggestions)
				{
					bool match = false;
					List<string> suggestionContents = suggestion.Categories.Concat(suggestion.Limitations).ToList();

					foreach(string filter in filters)
					{
						if (filter != "Mijn-suggesties")
						{
							if (suggestionContents.Contains(filter))
							{
								match = true;
							}
							else
							{
								match = false;
								break;
							}
						}
					}

					if (match) filtered.Add(suggestion);
				}
				suggestions = filtered;
			}
			return suggestions;
		}

		private List<Suggestion> SortSuggestions(string method, List<Suggestion> suggestions)
		{
			if (method == "popular")
			{
				suggestions = suggestions.OrderByDescending(suggestion => suggestion.Votes).ToList();
			}
			else if (method == "least-popular")
			{
				suggestions = suggestions.OrderBy(suggestion => suggestion.Votes).ToList();
			}
			else if (method == "trending")
			{
				suggestions = suggestions.OrderBy(suggestion => Guid.NewGuid()).ToList();
			}
			else if (method == "latest")
			{
				suggestions = suggestions.OrderByDescending(suggestion => DateTime.Parse(suggestion.Date)).ToList();
			}
			else if (method == "oldest")
			{
				suggestions = suggestions.OrderBy(suggestion => DateTime.Parse(suggestion.Date)).ToList();
			}
			return suggestions;
		}

		public bool SubmitSuggestion(string user, string name, string description, List<string> categories, List<string> limitations)
		{
			ActivityDataManager adm = new();
			adm.ActivitySubmit(name, description,limitations, categories, user);
			return true;
		}
	}
}
