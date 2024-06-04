using System.Data;
using BLL.Models;
using DAL;

namespace BLL
{
    public class ActivityManager
	{
		public bool ActivityPlanned(string team)
		{
			TeamDataManager tdm = new TeamDataManager();
			bool activityPlanned = tdm.CheckPlannedActivity(tdm.GetTeamId(team));
			return activityPlanned;
		}

		public BLL.Models.Activity GetActivity(string team)
        {
	        TeamDataManager tdm = new TeamDataManager();
			ActivityDataManager adm = new ActivityDataManager();
			int teamId = tdm.getPlannedActivityId(tdm.GetTeamId(team));
			DataRow activityData  = adm.GetActivity(teamId);
			DataTable limitationsData = adm.GetLimitations(teamId);
			DataTable categoriesData = adm.GetCategories(teamId);
			List<string> limitations = new List<string>();
			List<string> categories = new List<string>();

			foreach (DataRow row in limitationsData.Rows)
			{
				limitations.Add(row["limitation"].ToString());
			}

			foreach (DataRow row in categoriesData.Rows)
			{
				categories.Add(row["category"].ToString());
			}

			BLL.Models.Activity activity = new BLL.Models.Activity
			{
				Name = activityData["name"].ToString(),
				Description = activityData["description"].ToString(),
				Categories = categories,
				Limitations = limitations,
				Location = activityData["location"].ToString(),
				StartDate = activityData["start_date"].ToString()
			};

			return activity;
        }

		public void AddLocationToActivity(string location, int activityId)
		{
			ActivityDataManager adm = new();

			adm.AddLocationToActivity(activityId, location);
		}

		public bool RemoveActivityAfterDeadline(int activityId, DateTime deadline)
		{
			ActivityDataManager adm = new();

			if (deadline <= DateTime.Now.AddDays(1) && activityId != null)
			{
				adm.DeleteActivityById(activityId);
				return true;
			}
			return false;
		}

		public int GetPlannedActivityIdByTeamName(string teamName)
		{
			ActivityDataManager adm = new();
			TeamDataManager tdm = new();

			return adm.GetTeamActivityId(tdm.GetTeamId(teamName));
		}

		public List<Activity> GetAllActivities()
		{
			ActivityDataManager adm = new ActivityDataManager();
			DataTable activityData = adm.GetAllActivitys();
			List<Activity> activitys = new();

			foreach (DataRow ActivityRow in activityData.Rows)
			{
				DataTable limitationsData = adm.GetLimitations(Convert.ToInt32(ActivityRow["id"]));
				DataTable categoriesData = adm.GetCategories(Convert.ToInt32(ActivityRow["id"]));
				List<string> limitations = new List<string>();
				List<string> categories = new List<string>();

				foreach(DataRow row in limitationsData.Rows)
				{
					limitations.Add(row["limitation"].ToString());
				}

				foreach (DataRow row in categoriesData.Rows)
				{
					categories.Add(row["category"].ToString());
				}

				BLL.Models.Activity activity = new BLL.Models.Activity
				{
					Name = ActivityRow["name"].ToString(),
					Description = ActivityRow["description"].ToString(),
					Categories = categories,
					Limitations = limitations,
					Location = ActivityRow["location"].ToString(),
					StartDate = ActivityRow["start_date"].ToString()
				};

				activitys.Add(activity);
			}

			return activitys;
		}
    }
}
