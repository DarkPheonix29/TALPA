using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.VisualBasic;

namespace BLL
{
    internal class Poll(DateTime deadline)
    {
        private int ActivityVotes1 = 0;
        private int ActivityVotes2 = 0;
        private int ActivityVotes3 = 0;

        //Deze moet bij de activity komen
        private bool Won = false;

        public int TieBreakerNumber;
        public int TripleTieBreakerNumber;

        private DateTime CurrentTime = DateTime.Now;
        private DateTime Deadline { get; set; } = deadline;
        private List<int> activity_id { get; set; }

        private void Splitactivitiess(int poll_id)
        {
            if (activity_id.Count == 3)
            {
                ActivityVotes1 = activity_id[0];
                ActivityVotes2 = activity_id[1];
                ActivityVotes3 = activity_id[2];
            }
        }
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

        public void DeadlineCheck()
        {
            if (CurrentTime >= Deadline)
            {
                DecideWinner();
                GivePointsToWinner();
            }
        }

        private void DecideWinner()
        {
            if (ActivityVotes1 > ActivityVotes2 && ActivityVotes1 > ActivityVotes3)
            {
                // verwijst naar de activity die gewonnen heeft en zet won op true
                Won = true;
                
                
            }

            else if (ActivityVotes2 > ActivityVotes1 && ActivityVotes2 > ActivityVotes3)
            {
                // verwijst naar de activity die gewonnen heeft en zet won op true
                Won = true;
            }

            else if (ActivityVotes3 > ActivityVotes1 && ActivityVotes3 > ActivityVotes2)
            {
                // verwijst naar de activity die gewonnen heeft en zet won op true
                Won = true;
            }

            else
            {
                TieBreaker();
            }
        }

        private void TieBreaker()
        {
            if (ActivityVotes1 == ActivityVotes2 && ActivityVotes1 == ActivityVotes3)
            {
                GetTripleRandom();
                if (TieBreakerNumber == 0)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }

                else if (TieBreakerNumber == 1)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }

                else if (TieBreakerNumber == 2)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }
            }

            else if (ActivityVotes1 == ActivityVotes2)
            {
                GetDoubleRandom();
                if (TieBreakerNumber == 0)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }

                else if (TieBreakerNumber == 1)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }
            }

            else if (ActivityVotes1 == ActivityVotes3)
            {
                GetDoubleRandom();
                if (TieBreakerNumber == 0)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }

                else if (TieBreakerNumber == 1)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }
            }
            
            else if (ActivityVotes2 == ActivityVotes3)
            {
                GetDoubleRandom();
                if (TieBreakerNumber == 0)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }

                else if (TieBreakerNumber == 1)
                {
                    // verwijst naar de activity die gewonnen heeft en zet won op true
                    Won = true;
                }
            }
        }

        private void GetDoubleRandom()
        {
            Random rnd = new Random();
            TieBreakerNumber = rnd.Next(1);
        }

        private void GetTripleRandom()
        {
            Random rnd = new Random();
            TripleTieBreakerNumber = rnd.Next(2);
        }

        private void GivePointsToWinner()
        {

        }
    }
}

