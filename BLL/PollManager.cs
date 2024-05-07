using BLL.Models;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BLL
{
    public class PollManager
	{
		public bool PollActive(string team)
		{
			// team is de team naam, deze is uniek"

			bool pollActive = true; // is stemming actief voor team?

			return pollActive;
		}

		public bool PollChosen(string user)
		{
			// user is de user id, auth0|...

			bool pollChosen = false; // heeft de persoon al gestemd?

			return pollChosen;
		}

		public Poll GetPoll(string team)
        {
			// team is de team naam, deze is uniek"

			List<Suggestion>  suggestions = new List<Suggestion>(); // vull deze lijst de 3 suggesties in de poll
			int chosenSuggestion = 3; // id van de gekozen suggestie, als niks gekozen is, dan 0
			List<string> availability = new List<string> { "10-04-2024", "13-04-2024", "17-04-2024" }; // Belangrijk tijd in formaar [dd-mm-yy]
			List<string> possibleDates = new List<string> {  "11-5-2024", "18-5-2024", "25-5-2024", "1-6-2024" }; // Belangrijk tijd in formaar [dd-mm-yy]
			string deadline = "10-04-2024 15:30"; // Belangrijk tijd in formaar [dd-mm-yy hh:mm]

			suggestions.Add(new Suggestion
			{
				Id = 1,
				Name = "Stadswandeling 1",
				Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
				Categories = new List<string> { "Buiten", "Middag" },
				Limitations = new List<string> { "Tijd", "Alcohol" },
				Votes = 5 // Hoeveel stemmen heeft deze suggestie in deze poll?
			});
			suggestions.Add(new Suggestion
			{
				Id = 2,
				Name = "Stadswandeling 2",
				Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
				Categories = new List<string> { "Buiten", "Middag" },
				Limitations = new List<string> { "Tijd", "Alcohol" },
				Votes = 7 // Hoeveel stemmen heeft deze suggestie in deze poll?
			});
			suggestions.Add(new Suggestion
			{
				Id = 3,
				Name = "Stadswandeling 3",
				Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
				Categories = new List<string> { "Buiten", "Middag" },
				Limitations = new List<string> { "Tijd", "Alcohol" },
				Votes = 1 // Hoeveel stemmen heeft deze suggestie in deze poll?
			});

			Poll poll = new Poll
			{
				ChosenSuggestion = chosenSuggestion,
				Suggestions = suggestions,
				Availability = availability,
				PossibleDates = possibleDates,
				Deadline = deadline
			};

			return poll;
        }

		public bool SubmitPoll(string user, string team, int suggestion, List<string> availability)
		{
			// Sla keuze op
			Console.WriteLine($@"
				user: {user} 
				team: {team}
				suggestions:{string.Join(",", suggestion)}
				availability: {string.Join(",", availability)} 
			");
			return true;
		}

		public bool CreatePoll(string user, string team, List<string> activities, string deadline, List<string> availability)
		{
			// Maak poll aan
			Console.WriteLine($@"
				user: {user} 
				team: {team}
				deadline: {deadline}
				suggestions:{string.Join(",", activities)}
				availability: {string.Join(",", availability)} 
			");
			return false; // Return true als poll aangemaakt is en false als poll al bestaat voor team
		}
	}
}
