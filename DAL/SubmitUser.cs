using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SubmitUser
    {
        public static void UserSubmit(string email)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO [user] (username) OUTPUT INSERTED.id VALUES (@Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();

                        int userId = (int)command.ExecuteScalar();
                        Console.WriteLine(userId.ToString());
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error submitting activity.", ex);
                    }
                }
            }
        }
    }
}
