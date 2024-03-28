namespace TALPA.Models
{
    public class SuggestionViewModel
    {
        public string Activity { get; set; }
        public List<string> Restrictions { get; set; }

        public SuggestionViewModel()
        {
            Restrictions = new List<string>();
        }
    }
}
