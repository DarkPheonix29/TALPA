using BLL.Models;
namespace BLL
{
    public class SuggestionManager
    {
        public List<Suggestion> GetSuggestions()
        {
            List<Suggestion> suggestions = new List<Suggestion>(); // Vul lijst met alle suggesties

			for (int i = 1; i <= 15; i++)
			{
                Suggestion suggestion = new Suggestion
                {
					Id = i,
					Name = "Stadswandeling "+i,
					Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
					Categories = new List<string> { "Buiten", "Middag" },
					Limitations = new List<string> { "Tijd", "Alcohol" },
					Votes = 3*i // Hoeveel stemmen heeft deze suggestie in totaal?
				};
                suggestions.Add(suggestion);
			}

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

			limitations.Add("Water");
			limitations.Add("Toegankelijkheid");
			limitations.Add("Tijd");
			limitations.Add("Alcohol");

			return limitations;
		}
	}
}
