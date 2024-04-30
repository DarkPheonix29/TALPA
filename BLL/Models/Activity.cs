namespace BLL.Models
{
    public class Activity
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Categories { get; set; }
		public List<string> Limitations { get; set; }
		public string Location { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
	}
}
