using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public int SuggestionsSelected { get; set; }
        public int SuggestionVoted { get; set; }
        public int SuggestionChosen { get; set; }

        public Employee(string name, string email, int points, int suggestionsSelected, int suggestionVoted, int suggestionChosen)
        {
            Name = name;
            Email = email;
            Points = points;
            SuggestionsSelected = suggestionsSelected;
            SuggestionVoted = suggestionVoted;
            SuggestionChosen = suggestionChosen;
        }
    }
}
