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
            List<int> VoterId = new();
            List<int> limitationIDs = new();
            if (!activity.VotedUsers.IsNullOrEmpty())
                foreach (User user in activity.VotedUsers)
                {
                    VoterId.Add(user.Id);
                }
            if (!activity.Limitations.IsNullOrEmpty())
                foreach (LimitationTypes limit in activity.Limitations)
                {
                    limitationIDs.Add((int)limit);
                }
            ActivityDataManager.ActivitySubmit(activity.Name, activity.Description, activity.DateAdded, limitationIDs, activity.ProposingUser.Id);
        }
        public Activity constructActivityFromDB(int id)
        {
            List<LimitationTypes> limitations = new();
            DataTable adt = ActivityDataManager.GetActivity(id);
            DataTable ldt = ActivityDataManager.GetLimitations(id);
            DataRow row = adt.Rows[0];

            foreach (DataRow lrow in ldt.Rows)
            {
                int limitationId = (int)lrow["limitation_id"];
                limitations.Add((LimitationTypes)limitationId);
            }

            User proposingUser = new("", 0);
            Activity activity = new(Convert.ToString(row["name"]), Convert.ToString(row["description"]), limitations, proposingUser.ConstructUserFromDB(Convert.ToInt32(row["proposing_user"])), Convert.ToDateTime(row["date_added"]));
            return activity;
        }
    }
}
