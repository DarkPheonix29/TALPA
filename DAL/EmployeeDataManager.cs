using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class EmployeeDataManager
    {
        private DataAccess dataAccess;

        public EmployeeDataManager()
        {
            dataAccess = new DataAccess();
        }

        public int RegisterUser(string user, string name, string email)
        {
            int RowsAffected = dataAccess.ExecuteNonQuery($"INSERT IGNORE INTO employees (id, name, email) VALUES ('{user}', '{name}', '{email}');");
            return RowsAffected;
        }

        public DataTable GetUserTeam(string user)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM employees WHERE id = '{user}';");
            return data;
        }

        public DataTable GetTeamEmployees(int team_id)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM employees WHERE team = '{team_id}';");
            return data;
        }
    }
}