using System.Data;
using BLL.Models;
using System.Globalization;
using DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Data.SqlClient;
namespace BLL
{
    public class PollManager
	{
		public bool PollActive(string team)
		{
			TeamDataManager tdm = new TeamDataManager();
			PollDataManager pdm = new PollDataManager();
			if (team != null)
			{
				DataRow poll = pdm.GetPollOfTeam(tdm.GetTeamId(team));

				if (poll == null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
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
			var adm = new ActivityDataManager();
			var tdm = new TeamDataManager();
			var pdm = new PollDataManager();
			var udm = new UserDataManager();

			int teamId = tdm.GetTeamId(team);
			DataRow pollId = pdm.GetPollOfTeam(teamId);
			int pollIdInt = Convert.ToInt32(pollId["id"]);
			DataTable selection = pdm.GetSelection(pollIdInt);

			int chosenSuggestion = GetChosenSuggestion(adm, selection);
			List<Suggestion> suggestions = GetSuggestions(adm, selection);

			Poll poll = new Poll
			{
				ChosenSuggestion = chosenSuggestion,
				Suggestions = suggestions,
				Availability = udm.getVoteDates(udm.getVoteId(user)),
				PossibleDates = pdm.getPollDates(pollIdInt),
				Deadline = pollId["deadline"].ToString()
			};

			return poll;
		}

		private int GetChosenSuggestion(ActivityDataManager adm, DataTable selection)
		{
			foreach (DataRow row in selection.Rows)
			{
				DataRow activity = adm.GetActivity(Convert.ToInt32(row["activity_id"]));
				if (Convert.ToBoolean(activity["has_been_chosen"]))
				{
					return Convert.ToInt32(row["activity_id"]);
				}
			}
			return 0; // Default value if no chosen suggestion is found
		}

		private List<Suggestion> GetSuggestions(ActivityDataManager adm, DataTable selection)
		{
			var suggestions = new List<Suggestion>();

			foreach (DataRow row in selection.Rows)
			{
				int activityId = Convert.ToInt32(row["activity_id"]);
				DataRow activity = adm.GetActivity(activityId);

				List<string> votedUsers = adm.GetVotedUsers(activityId);
				List<string> limitations = GetLimitations(adm, activityId);
				List<string> categories = GetCategories(adm, activityId);

				var suggestion = new Suggestion
				{
					Id = activityId,
					Name = activity["name"].ToString(),
					Description = activity["description"].ToString(),
					Categories = categories,
					Limitations = limitations,
					Votes = votedUsers.Count
				};

				suggestions.Add(suggestion);
			}

			return suggestions;
		}

		private List<string> GetLimitations(ActivityDataManager adm, int activityId)
		{
			DataTable limitationsData = adm.GetLimitations(activityId);
			var limitations = new List<string>();

			foreach (DataRow row in limitationsData.Rows)
			{
				limitations.Add(row["limitation"].ToString());
			}

			return limitations;
		}

		private List<string> GetCategories(ActivityDataManager adm, int activityId)
		{
			DataTable categoriesData = adm.GetCategories(activityId);
			var categories = new List<string>();

			foreach (DataRow row in categoriesData.Rows)
			{
				categories.Add(row["category"].ToString());
			}

			return categories;
		}

		public bool SubmitPoll(string user, int suggestion, List<string> availability, int pollId)
		{
			PollDataManager pdm = new();

			pdm.CreateVote(suggestion, user, availability, pollId);

			return true;
		}

		public bool CreatePoll(string team, List<int> activities, string date, List<string> Availability)
		{
			TeamDataManager tdm = new();
			PollDataManager pdm = new();

			try
			{
				pdm.PollSubmit(tdm.GetTeamId(team), date, activities, Availability);
				return true;
			}
			catch
			{
				return false;
			}
		}

    public bool EndPoll(string team)
    {
        TeamDataManager tdm = new TeamDataManager();
        PollDataManager pdm = new PollDataManager();
        ActivityDataManager adm = new ActivityDataManager();
        UserDataManager udm = new UserDataManager();

        int teamId = tdm.GetTeamId(team);
        DataRow poll = pdm.GetPollOfTeam(teamId);
        if (poll != null)
        {
            DateTime deadline = DateTime.Parse(poll["deadline"].ToString());
            if (DateTime.Now > deadline)
            {
                int pollId = Convert.ToInt32(poll["id"]);
                DataTable selection = pdm.GetSelection(pollId);

                int maxVotes = 0;
                int selectedActivityId = 0;
                foreach (DataRow row in selection.Rows)
                {
                    int activityId = Convert.ToInt32(row["activity_id"]);
                    int votes = adm.GetVotedUsers(activityId).Count;
                    if (votes > maxVotes)
                    {
                        maxVotes = votes;
                        selectedActivityId = activityId;
                    }
                }

                if (selectedActivityId > 0)
                {
                    adm.ChooseActivity(selectedActivityId, pdm.GetDateOfWinningActivity(pdm.GetVoteIdWithActivityId(selectedActivityId)));

                    // Retrieve the user who created the winning activity
                    string creatorId = adm.GetActivityCreator(selectedActivityId);

                    // Update the points of the creator
                    udm.AddPointsToUser(creatorId, maxVotes);
                }

                pdm.DeletePoll(teamId);
                return true;
            }
        }

        return false;
    }

		public int GetPollIdWithTeamName(string teamName)
		{
			PollDataManager pdm = new();
			TeamDataManager tdm = new();

			return pdm.GetPollIdWithTeamId(tdm.GetTeamId(teamName));
		}
	}

}
