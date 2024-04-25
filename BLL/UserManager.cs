using DAL;
using System.Data;

namespace BLL
{
    internal class UserManager
    {
        public User ConstructUserFromDB(string id)
        {
	        UserDataManager udm = new();
            DataTable dt = udm.GetUser(id);
            DataRow row = dt.Rows[0];
            User user = new(Convert.ToInt32(row["points"]));
            return user;
        }
    }
}
