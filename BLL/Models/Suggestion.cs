namespace BLL.Models
{
	public class Suggestion
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> Categories { get; set; }
		public List<string> Limitations { get; set; }
		public int Votes { get; set; }
		public string Date { get; set; }
	}
}
