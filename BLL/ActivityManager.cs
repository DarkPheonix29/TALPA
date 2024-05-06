using BLL.Models;
namespace BLL
{
    public class ActivityManager
	{
		public bool ActivityPlanned(string team)
		{
			// team is de team naam, deze is uniek"

			bool activityPlanned = false; // is uitje gepland voor team?

			return activityPlanned;
		}

		public Activity GetActivity(string team)
        {
            // team is de team naam, deze is uniek"

            Activity activity = new Activity
            {
				Name = "Stadswandeling",
				Description = "Verken de bezienswaardigheden en verborgen juweeltjes van de stad tijdens een ontspannen wandeling met je collega's.",
				Categories = new List<string> { "Buiten", "Middag" },
				Limitations = new List<string> { "Tijd", "Alcohol" },
				Location = "Molendijk 6, 6107 AA Stevensweert",
				StartDate = "10-04-2024 17:00", // Belangrijk tijd in formaar [dd-mm-yy hh:mm]
				EndDate = "10-04-2024 19:30" // Belangrijk tijd in formaar [dd-mm-yy hh:mm]
			};

			return activity;
        }
    }
}
