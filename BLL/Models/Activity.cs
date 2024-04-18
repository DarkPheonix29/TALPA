using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
        public List<Restriction> Restrictions { get; set; }

        public Activity(int id, string name, string description, List<string> categories, List<Restriction> restrictions)
        {
            Id = id;
            Name = name;
            Description = description;
            Categories = categories;
            Restrictions = restrictions;
        }
    }
}
