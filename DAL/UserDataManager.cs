using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDataManager
    {
        public static void UserSubmit(string email)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO [user] (email) OUTPUT INSERTED.id VALUES (@Email)";
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

        public static DataTable GetUser(int id)
        {
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT * FROM user WHERE id = @UserId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@UserId", id);

						connection.Open();
						DataTable dt = new();
						using (SqlDataAdapter da = new(command))
						{
							da.Fill(dt);
						}

						return dt;
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error fetching user.", ex);
					}
				}
			}
		}
    }
}
