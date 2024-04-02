namespace TALPA.Models
{
    public class SuggestionViewModel
    {
        public string Activity { get; set; }
        public List<string> limitations { get; set; }

        public SuggestionViewModel()
        {
            limitations = new List<string>();
        }
    }
}
