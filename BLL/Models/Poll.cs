namespace BLL.Models
{
	public class Poll
	{
		public string Deadline { get; set; }
		public int ChosenSuggestion { get; set; }
		public List<Suggestion> Suggestions { get; set; }
		public List<string> Availability { get; set; }
	}
}
