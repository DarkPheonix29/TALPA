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

        public void Vote(User VotingUser)
        {
            VotedUsers.Add(VotingUser);
        }
    }
}
