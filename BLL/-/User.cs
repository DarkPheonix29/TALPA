namespace BLL
{
    public class User(int points)
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        private int Points { get; set; } = points;



        public void PointsAmount(int initialBalance)
        {
            Points = initialBalance;
        }

        public void Pointsreceive(int amount)
        {
            Points += amount;
        }

        public int SeeAmount(int amount)
        {
            return Points;
        }
    }
}
