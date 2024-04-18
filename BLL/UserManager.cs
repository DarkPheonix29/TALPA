using DAL;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    internal class UserManager
    {
        public User ConstructUserFromDB(string id)
        {
            
            DataTable dt = UserDataManager.GetUser(id);
            DataRow row = dt.Rows[0];
            User user = new(Convert.ToInt32(row["points"]));
            return user;
        }
    }
}
