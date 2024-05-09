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
			DataRow poll = pdm.GetPollOfTeam(tdm.GetTeamId(team));

			if (Convert.ToInt32(poll["id"]) == 0)
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

		public Poll GetPoll(string user, string team)
        {
			ActivityDataManager adm = new();
			TeamDataManager tdm = new();
			PollDataManager pdm = new();
			UserDataManager udm = new();

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

			Poll poll = new Poll
			{
				ChosenSuggestion = chosenSuggestion,
				Suggestions = suggestions,
				Availability = udm.getVoteDates(udm.getVoteId(user)),
				PossibleDates = pdm.getPollDates(Convert.ToInt32(pollId["id"])),
				Deadline = pollId["deadline"].ToString()
			};

			return poll;
        }

		public bool SubmitPoll(string user, int suggestion, List<string> availability)
		{
			PollDataManager pdm = new();

			pdm.CreateVote(suggestion, user, availability);

			return true;
		}

		public bool CreatePoll(string team, List<int> activities, string date)
		{
			// activities needs to be a list of the id's, this could be done on this side if that is more convenient(I don't know if you already have the id's in the frond end).
			TeamDataManager tdm = new();
			PollDataManager pdm = new();

			try
			{
				pdm.PollSubmit(tdm.GetTeamId(team), date, activities);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
