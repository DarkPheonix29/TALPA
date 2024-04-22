using System.Collections.Generic;
using BLL.Models;

namespace TALPA.Models
{
    public class ActivityViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Suggestion> Suggestions { get; set; }
    }
}
