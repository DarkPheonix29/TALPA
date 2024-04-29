

using BLL.Models;
namespace TALPA.Models
{
    public class ActivityViewModel
	{
        public bool ActivityPlanned { get; set; }
		public string ActivityName { get; set; }
		public string ActivityDescription { get; set; }
		public List<string> ActivityCategories { get; set; }
		public List<string> ActivityLimitation { get; set; }
		public string ActivityLocation { get; set; }
		public string ActivityStartDate { get; set; }
		public string ActivityEndDate { get; set; }
	}
}
