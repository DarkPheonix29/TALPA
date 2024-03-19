using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    internal class Activity(string name, string description, List<string> limitations, User proposingUser, DateTime dateAdded)
    {
        private string Name { get; set; } = name;
        private string Description { get; set; } = description;
        private DateTime DateAdded { get; set; } = dateAdded;
        private List<string> Limitations { get; set; } = limitations;
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
            List<int> VoterId = new List<int>();
            foreach (User user in VotedUsers)
            {
                VoterId.Add(user.id);
            }
            SubmitActivity.ActivitySubmit(name, description, dateAdded, limitations, ProposingUser.id, VoterId);
        }
    }
}
