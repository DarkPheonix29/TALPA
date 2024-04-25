using Microsoft.Data.SqlClient;
using System.Data;


namespace DAL
{
    public class UserDataManager
    {
        public void UserSubmit(string id)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO [user] (id) VALUES (@Id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@id", id);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error submitting user.", ex);
                    }
                }
            }
        }

        public DataTable GetUser(string id)
        {
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT * FROM [user] WHERE id = @UserId";
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
						return null;
					}
				}
			}
		}
    }
}
