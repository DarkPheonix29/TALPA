using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PollManager
    {
        private readonly PollDataManager _pollDataManager;
        public PollManager()
        {
            _pollDataManager = new PollDataManager();
        }

        public bool CreatePoll(int team, List<string> dates, List<int> activities)
        {
            int id = _pollDataManager.CreatePoll(team, activities[0], activities[1], activities[2]);

            foreach (string date in dates)
            {
                _pollDataManager.SubmitDate(team, date);
            }

            return id != null;
        }

        public Poll getCurrentPoll(int team) 
        {
            bool active = false;
            string deadline = "";
            string activity1Name = "";
            string activity2Name = "";
            string activity3Name = "";
            int activity1Id = 0;
            int activity2Id = 0;
            int activity3Id = 0;
            int activity1Votes = 0;
            int activity2Votes = 0;
            int activity3Votes = 0;

            DataTable result = _pollDataManager.GetPoll(team);
            if (result.Rows.Count > 0)
            {
                active = true;
                activity1Id = Convert.ToInt32(result.Rows[0]["activity_1"]);
                activity2Id = Convert.ToInt32(result.Rows[0]["activity_2"]);
                activity3Id = Convert.ToInt32(result.Rows[0]["activity_3"]);
                activity1Name = _pollDataManager.GetPollName(activity1Id);
                activity2Name = _pollDataManager.GetPollName(activity2Id);
                activity3Name = _pollDataManager.GetPollName(activity3Id);
            }

            return new Poll(active, deadline, activity1Name, activity2Name, activity3Name, activity1Id, activity2Id, activity3Id, activity1Votes, activity2Votes, activity3Votes);
        }
    }
}
