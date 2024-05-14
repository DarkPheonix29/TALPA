using DAL;

namespace BLL
{
    public class EmployeeManager
    {
        public int GetPoints(string user, string team)
        {
            UserDataManager udm = new();
            TeamDataManager tdm = new();
	        int points = udm.GetPoints(user);

	        if (!udm.UserExists(user))
	        {
				udm.UserSubmit(user);
	        }

	        if (!tdm.TeamExists(team))
	        {
				tdm.CreateTeam(team);
	        }

			return points;
		}
    }
}
