using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmployeeManager
    {
        private readonly EmployeeDataManager _employeeDataManager;
        public EmployeeManager()
        {
            _employeeDataManager = new EmployeeDataManager();
        }

        public bool RegisterUser(string user, string name, string email)
        {
            int rowsAffected = _employeeDataManager.RegisterUser(user, name, email);

            return rowsAffected >= 1;
        }

        public int GetUserTeam(string user)
        {
            DataTable result = _employeeDataManager.GetUserTeam(user);
            int team = Convert.ToInt32(result.Rows[0]["team"]);

            return team;
        }

        public List<Employee> GetTeamEmployees(int team_id)
        {
            List<Employee> employees = new List<Employee>();
            DataTable result = _employeeDataManager.GetTeamEmployees(team_id);
            foreach (DataRow row in result.Rows)
            {
                employees.Add(new Employee(row["name"].ToString(), row["email"].ToString(), 0, 0, 0, 0));
            }

            return employees;
        }
    }
}
