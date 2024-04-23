namespace TALPA.Models
{
    public class CreatePollViewModel
    {
        public List<string> Dates { get; set; }
        public List<string> Activities { get; set; }

        public CreatePollViewModel()
        {
            Dates = new List<string>();
            Activities = new List<string>();
        }
    }
}
