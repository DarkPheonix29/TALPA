using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Poll
    {
        public bool Active { get; set; }
        public string Deadline { get; set; }
        public string Activity1Name { get; set; }
        public string Activity2Name { get; set; }
        public string Activity3Name { get; set; }
        public int Activity1Id { get; set; }
        public int Activity2Id { get; set; }
        public int Activity3Id { get; set; }
        public int Activity1Votes { get; set; }
        public int Activity2Votes { get; set; }
        public int Activity3Votes { get; set; }

        public Poll(bool active, string deadline, string activity1Name, string activity2Name, string activity3Name, int activity1Id, int activity2Id, int activity3Id, int activity1Votes, int activity2Votes, int activity3Votes)
        { 
            Active = active;
            Deadline = deadline;
            Activity1Name = activity1Name;
            Activity2Name = activity2Name;
            Activity3Name = activity3Name;
            Activity1Id = activity1Id;
            Activity2Id = activity2Id;
            Activity3Id = activity3Id;
            Activity1Votes = activity1Votes;
            Activity2Votes = activity2Votes;
            Activity3Votes = activity3Votes;
        }
    }
}
