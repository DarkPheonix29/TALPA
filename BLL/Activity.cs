using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.IdentityModel.Tokens;

namespace BLL
{
    public class Activity(string name, string description, List<LimitationTypes> limitations, User proposingUser, DateTime dateAdded)
    {
        private string Name { get; set; } = name;
        private string Description { get; set; } = description;
        private DateTime DateAdded { get; set; } = dateAdded;
        private List<LimitationTypes> Limitations { get; set; } = limitations;
        private User ProposingUser { get; set; } = proposingUser;
        private List<User> VotedUsers { get; set; }

        public bool Vote(User VotingUser)
        {
            bool alreadyExist = VotedUsers.Contains(VotingUser);
            if (!alreadyExist)
            {
                VotedUsers.Add(VotingUser);
            }
            return alreadyExist;
        }
        public void SubmitToDatabase()
        {
            List<int> VoterId = new();
            List<int> limitationIDs = new();
            if (!VotedUsers.IsNullOrEmpty() )
                foreach (User user in VotedUsers)
                {
                    VoterId.Add(user.Id);
                }
            if (!limitations.IsNullOrEmpty())
                foreach (LimitationTypes limit in limitations)
                {
                    limitationIDs.Add((int)limit);
                }
            ActivityDataManager.ActivitySubmit(name, description, dateAdded, limitationIDs , ProposingUser.Id);
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
