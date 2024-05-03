using BLL.Models;
namespace BLL
{
    public class SuggestionManager
    {
        public List<Suggestion> GetSuggestions(string search, string sort, List<string> filter)
        {
            List<Suggestion> suggestions = new List<Suggestion>(); // Vul lijst met alle suggesties

			for (int i = 1; i <= 55; i++)
			{
                Suggestion suggestion = new Suggestion
                {
					Id = i,
					Name = "Stadswandeling "+i,
					Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
					Categories = new List<string> { "Buiten", "Middag" },
					Limitations = new List<string> { "Tijd", "Alcohol" },
					Votes = 3*i, // Hoeveel stemmen heeft deze suggestie in totaal?
					Date = DateTime.Now.Add(TimeSpan.FromDays(new Random().Next(0, DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))).ToString("yyyy-MM-dd HH:mm:ss") // Aanmaak datum, DateTime formaat van mysql.
			};
                suggestions.Add(suggestion);
			}

			suggestions = SearchSuggestions(search, suggestions);
			if (!string.IsNullOrWhiteSpace(filter[0]))
			{
				suggestions = FilterSuggestions(filter, suggestions);
			}
			suggestions = SortSuggestions(sort, suggestions);

			return suggestions;
        }

		public List<string> GetCategories()
		{
			List<string> categories = new List<string>(); // Vul lijst met alle catagorien elk 1x

			categories.Add("Buiten");
			categories.Add("Binnen");
			categories.Add("Middag");
			categories.Add("Avond");
			categories.Add("Water");

			return categories;
		}

		public List<string> GetLimitations()
		{
			List<string> limitations = new List<string>(); // Vul lijst met alle catagorien elk 1x

			limitations.Add("Toegankelijkheid");
			limitations.Add("Tijd");
			limitations.Add("Alcohol");

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
			suggestions = suggestions.Where(
				suggestion => suggestion.Limitations.Any(limitation => filters.Contains(limitation)) ||
				suggestion.Categories.Any(category => filters.Contains(category))
			).ToList();
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
	}
}
