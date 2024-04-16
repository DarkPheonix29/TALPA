using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.VisualBasic;

namespace BLL
{
    internal class Poll(DateTime deadline)
    {
        public int ActivityVotes1 = 0;
        public int ActivityVotes2 = 0;
        public int ActivityVotes3 = 0;

        //Deze moet bij de activity komen
        public bool Won = false;

        public int TieBreakerNumber;
        public int TripleTieBreakerNumber;

        public DateTime CurrentTime = DateTime.Now;
        public DateTime Deadline { get; set; } = deadline;
        public List<int> activity_id { get; set; }
    }
}

