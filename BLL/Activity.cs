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
    public class Activity(string name, string description, List<LimitationTypes> limitations, string proposingUserId, DateTime dateAdded)
    {
        public int ActivityID { get; set; }
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public DateTime DateAdded { get; set; } = dateAdded;
        public List<LimitationTypes> Limitations { get; set; } = limitations;
        public string ProposingUserId { get; set; } = proposingUserId;
        public List<User> VotedUsers { get; set; }

        public bool Vote(User VotingUser)
        {
            bool alreadyExist = VotedUsers.Contains(VotingUser);
            if (!alreadyExist)
            {
                VotedUsers.Add(VotingUser);
            }
            return alreadyExist;
        }
    }
}
