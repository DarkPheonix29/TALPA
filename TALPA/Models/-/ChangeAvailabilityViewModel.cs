namespace TALPA.Models
{
    public class ChangeAvailabilityViewModel
    {
        public List<string> AvailableDates { get; set; }

        public ChangeAvailabilityViewModel()
        {
            AvailableDates = new List<string>();
        }
    }
}