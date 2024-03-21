using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
        private int Points { get; set; }

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
