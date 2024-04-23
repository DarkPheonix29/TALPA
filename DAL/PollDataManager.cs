using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class PollDataManager
    {
        private DataAccess dataAccess;

        public PollDataManager()
        {
            dataAccess = new DataAccess();
        }

        public int CreatePoll(int team, int activity1, int activity2, int activity3)
        {
            object id = dataAccess.ExecuteScalarQuery($"INSERT IGNORE INTO polls (team, activity_1, activity_2, activity_3) VALUES ({team}, {activity1}, {activity2}, {activity3}); SELECT LAST_INSERT_ID();");
            return Convert.ToInt32(id);
        }

        public int SubmitDate(int team, string date)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO poll_date (team, date) VALUES ({team}, '{date}');");
            return rowsAffected;
        }

        public DataTable GetPoll(int team)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM polls WHERE team = {team};");
            return data;
        }

        public string GetPollName(int poll)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM suggestions WHERE id = {poll};");
            string name = data.Rows[0]["suggestion"].ToString();
            return name;
        }
    }
}