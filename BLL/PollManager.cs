using System.Data;
using BLL.Models;
using System.Globalization;
using DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BLL
{
    public class PollManager
	{
		public bool PollActive(string team)
		{
			TeamDataManager tdm = new TeamDataManager();
			PollDataManager pdm = new PollDataManager();
			int pollId = pdm.GetPollIdOfTeam(tdm.GetTeamId(team));

			if (pollId == 0 )
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool PollChosen(string user)
		{
			UserDataManager udm = new UserDataManager();

			List<int> id = udm.GetVotedActivities(user);

			if (id.Count == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public Poll GetPoll(string team)
        {
			ActivityDataManager adm = new();
			TeamDataManager tdm = new();
			PollDataManager pdm = new();

			DataRow pollId = pdm.GetPollOfTeam(tdm.GetTeamId(team));
			DataTable selection = pdm.GetSelection(Convert.ToInt32(pollId["id"]));

			int chosenSuggestion = 0;
			List<Suggestion> suggestions = new();
			foreach (DataRow row in selection.Rows)
			{
				if (Convert.ToInt32(row["has_been_chosen"]) == 1)
				{
					chosenSuggestion = Convert.ToInt32(row["activity_id"]);
				}
				List<string> votedUsers = adm.GetVotedUsers(Convert.ToInt32(row["activity_id"]));
				DataRow activity = adm.GetActivity(Convert.ToInt32(row["activity_id"]));

				DataTable limitationsData = adm.GetLimitations(Convert.ToInt32(row["activity_id"]));
				List<string> limitations = new();
				DataTable categoriesData = adm.GetCategories(Convert.ToInt32(row["activity_id"]));
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
					Id = Convert.ToInt32(row["activity_id"]),
					Name = activity["name"].ToString(),
					Description = activity["description"].ToString(),
					Categories = categories,
					Limitations = limitations,
					Votes = votedUsers.Count
				};
				suggestions.Add(suggestion);
			}

			List<string> availability = new List<string> { "10-04-2024", "13-04-2024", "17-04-2024" }; // Belangrijk tijd in formaar [dd-mm-yy]
			List<string> possibleDates = new List<string> {  "11-5-2024", "18-5-2024", "25-5-2024", "1-6-2024" }; // Belangrijk tijd in formaar [dd-mm-yy]

			Poll poll = new Poll
			{
				ChosenSuggestion = chosenSuggestion,
				Suggestions = suggestions,
				Availability = availability,
				PossibleDates = possibleDates,
				Deadline = pollId["deadline"].ToString()
			};

			return poll;
        }

		public bool SubmitPoll(string user, string team, int suggestion, List<string> availability)
		{
			// Sla keuze op
			Console.WriteLine($"{user} van {team}: {suggestion} | {string.Join(", ", availability)}");
			return true;
		}

		public bool CreatePoll(string user, string team, List<string> activities, string date)
		{
			// Maak poll aan
			Console.WriteLine($"{user} van {team}: {date} | {string.Join(",", activities)}");
			return false; // Return true als poll aangemaakt is en false als poll al bestaat voor team
		}
	}
}
