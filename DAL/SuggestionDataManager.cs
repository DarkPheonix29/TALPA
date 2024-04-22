using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SuggestionDataManager
    {
        private DataAccess dataAccess;

        public SuggestionDataManager()
        {
            dataAccess = new DataAccess();
        }

        public int SubmitSuggestion(string user, string suggestion, string description, string time)
        {
            object id = dataAccess.ExecuteScalarQuery($"INSERT INTO suggestions (user, suggestion, description, time) VALUES ('{user}', '{suggestion}', '{description}', '{time}'); SELECT LAST_INSERT_ID();");
            return Convert.ToInt32(id);
        }

        public int SubmitLimitation(int suggestion_id, string limitation, string description)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO suggestion_limitation (suggestion_id, limitation, description) VALUES ({suggestion_id}, '{limitation}', '{description}');");
            return rowsAffected;
        }

        public int SubmitCategorie(int suggestion_id, string categorie, string description)
        {
            int rowsAffected = dataAccess.ExecuteNonQuery($"INSERT INTO suggestion_categorie (suggestion_id, categorie, description) VALUES ({suggestion_id}, '{categorie}', '{description}');");
            return rowsAffected;
        }

        public DataTable GetUserSuggestions(string user)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM suggestions WHERE user = '{user}' ORDER BY time DESC;");
            return data;
        }

        public DataTable GetSuggestions()
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM suggestions ORDER BY time DESC;");
            return data;
        }

        public DataTable GetSuggestionLimitations(int suggestion_id)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM suggestion_limitation WHERE suggestion_id = '{suggestion_id}';");
            return data;
        }

        public DataTable GetSuggestionCategories(int suggestion_id)
        {
            DataTable data = dataAccess.ExecuteQuery($"SELECT * FROM suggestion_categorie WHERE suggestion_id = '{suggestion_id}';");
            return data;
        }
    }
}