using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess()
        {
            connectionString = SQLConnectionManager.GetConnectionString();
        }

        public DataTable ExecuteQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    return dataTable;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);
                return null;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing non-query: " + ex.Message);
                return 0;
            }
        }

        public object ExecuteScalarQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing scalar query: " + ex.Message);
                return null;
            }
        }
    }
}
