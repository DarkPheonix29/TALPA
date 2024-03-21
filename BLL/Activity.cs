using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.IdentityModel.Tokens;

namespace BLL
{
    public class Activity(string name, string description, List<int> limitationIDs, User proposingUser, DateTime dateAdded)
    {
        private string Name { get; set; } = name;
        private string Description { get; set; } = description;
        private DateTime DateAdded { get; set; } = dateAdded;
<<<<<<< HEAD
        private List<int> Limitations { get; set; } = limitationIDs;
=======

        private List<Limits> Limitations = new List<Limits>();
>>>>>>> main
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
            if (!VotedUsers.IsNullOrEmpty() )
                foreach (User user in VotedUsers)
                {
                    VoterId.Add(user.id);
                }
            SubmitActivity.ActivitySubmit(name, description, dateAdded, limitationIDs, ProposingUser.id);
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
