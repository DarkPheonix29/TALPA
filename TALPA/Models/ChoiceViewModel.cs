namespace TALPA.Models
{
    public class ChoiceViewModel
    {
        public string Activity { get; set; }
        public List<string> AvailableDates { get; set; }

        public ChoiceViewModel()
        {
            AvailableDates = new List<string>();
        }
    }
}
