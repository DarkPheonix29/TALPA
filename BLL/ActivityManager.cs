﻿using System.Data;
using System.Diagnostics;
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
				StartDate = activityData["start_date"].ToString(),
				EndDate = activityData["end_date"].ToString()
			};

			return activity;
        }
    }
}
