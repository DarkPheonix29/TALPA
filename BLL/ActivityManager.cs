using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace BLL
{
    public class ActivityManager
    {
        public void SubmitToDatabase(Activity activity)
        {
            List<string> VoterId = new();
            List<int> limitationIDs = new();
            if (!activity.VotedUsers.IsNullOrEmpty())
                foreach (User user in activity.VotedUsers)
                {
                    VoterId.Add(user.UserId);
                }
            if (!activity.Limitations.IsNullOrEmpty())
                foreach (LimitationTypes limit in activity.Limitations)
                {
                    limitationIDs.Add((int)limit);
                }
            ActivityDataManager.ActivitySubmit(activity.Name, activity.Description, activity.DateAdded, limitationIDs, activity.ProposingUserId, activity.Dates);
        }
        public Activity constructActivityFromDB(int id)
        {
            List<LimitationTypes> limitations = new();
            List<DateTime> dates = new();
            DataTable adt = ActivityDataManager.GetActivity(id);
            DataTable ldt = ActivityDataManager.GetLimitations(id);
            DataTable ddt = ActivityDataManager.GetDates(id);
            DataRow row = adt.Rows[0];

            foreach (DataRow lrow in ldt.Rows)
            {
                int limitationId = (int)lrow["limitation_id"];
                limitations.Add((LimitationTypes)limitationId);
            }

            foreach (DataRow drow in ddt.Rows)
            {
				dates.Add(Convert.ToDateTime(drow));
			}

            UserManager proposingUser = new();
            Activity activity = new(Convert.ToString(row["name"]), Convert.ToString(row["description"]), limitations, proposingUser.ConstructUserFromDB(Convert.ToString(row["proposing_user"])).UserId, Convert.ToDateTime(row["date_added"]));
            activity.Dates = dates;
            return activity;
        }

        public void chooseActivity(int id)
        {
	        ActivityDataManager adm = new();
	        adm.ChooseActivity(id);
        }
    }
}
