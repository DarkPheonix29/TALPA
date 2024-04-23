using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Limitations { get; set; }
        public List<string> Categories { get; set; }
        public int Selected { get; set; }
        public int Voted { get; set; }
        public int Chosen { get; set; }

        public Suggestion(int id, string name, string description, List<string> limitations, List<string> categories, int selected, int voted, int chosen)
        {
            Id = id;
            Name = name;
            Description = description;
            Limitations = limitations;
            Categories = categories;
            Selected = selected;
            Voted = voted;
            Chosen = chosen;
        }
    }
}
