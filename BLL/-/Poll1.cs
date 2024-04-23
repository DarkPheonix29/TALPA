using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.VisualBasic;

namespace BLL
{
    internal class Poll1(DateTime deadline, List<int> activity_id)
    {
        public int ActivityVotes1 = 0;
        public int ActivityVotes2 = 0;
        public int ActivityVotes3 = 0;

        //Deze moet bij de activity komen
        public bool Won = false;

        public int TieBreakerNumber;
        public int TripleTieBreakerNumber;

        public DateTime Deadline { get; set; } = deadline;
        public List<int> Activity_Id { get; set; } = activity_id;
    }
}

