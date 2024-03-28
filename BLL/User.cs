using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;

namespace BLL
{
    public class User(string name, int id)
    {
        public string Name { get; set; } = name;
        public int Id { get; set; } = id;

        public User ConstructUserFromDB(int id)
        {
            DataTable dt = UserDataManager.GetUser(id);
            DataRow row = dt.Rows[0];
			User user = new(Convert.ToString(row["name"]), Convert.ToInt32(row["id"]));
            return user;
        }
    }
}
