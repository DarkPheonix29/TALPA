using System.Diagnostics;

namespace BLL
{
    internal class Poll (DateTime deadline, Activity activity1, Activity activity2, Activity activity3)
    {
        public int ActivityVotes1 = 0;
        public int ActivityVotes2 = 0;
        public int ActivityVotes3 = 0;
        private DateTime Deadline { get; set; } = deadline;
        private Activity Activity1 { get; set; } = activity1;
        private Activity Activity2 { get; set; } = activity2;
        private Activity Activity3 { get; set; } = activity3;

        public void ActivityVoted(int VotedActivity)
        {
            switch (VotedActivity)
            {
                case 1:
                    ActivityVotes1++;
                    break;
                case 2: 
                    ActivityVotes2++; 
                    break;
                case 3:
                    ActivityVotes3++;
                    break;
            }
        }
    }
}

