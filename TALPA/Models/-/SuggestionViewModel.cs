namespace TALPA.Models
{
    public class SuggestionViewModel
    {
        public string Activity { get; set; }
        public string Description { get; set; }
        public List<string> Limitations { get; set; }
        public List<string> Categories { get; set; }

        public SuggestionViewModel()
        {
            Limitations = new List<string>();
            Categories = new List<string>();
        }
    }
}
