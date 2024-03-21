using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class Activity(string name, string description, List<string> limitations, User proposingUser, DateTime dateAdded)
    {
        private string Name { get; set; } = name;
        private string Description { get; set; } = description;
        private DateTime DateAdded { get; set; } = dateAdded;

        private List<Limits> Limitations = new List<Limits>();
        private User ProposingUser { get; set; } = proposingUser;
        private List<User> VotedUsers { get; set; }

        public void Vote(User VotingUser)
        {
            VotedUsers.Add(VotingUser);
        }

        public void CreateLimitations(List<string> limitations)
        {
            foreach (var limitation in limitations)
            {
                Limitations.Add(new Limits { Description = limitation, Type = "" });
            }
        }
    }
}
