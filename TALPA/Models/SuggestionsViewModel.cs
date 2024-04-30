using BLL.Models;
namespace TALPA.Models
{
    public class SuggestionsViewModel
	{
		public List<Suggestion> Suggestions { get; set; }
		public List<string> Categories { get; set; }
		public List<string> Limitations { get; set; }
	}
}