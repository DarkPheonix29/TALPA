using DAL;

namespace BLL
{
    public class EmployeeManager
    {
        public int GetPoints(string user)
        {
            UserDataManager udm = new UserDataManager();
	        int points = udm.GetPoints(user);

	        bool userExists = udm.UserExists(user);
	        if (!userExists)
	        {
				udm.UserSubmit(user);
	        }

			return points;
		}
    }
}
