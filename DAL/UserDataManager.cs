using Microsoft.Data.SqlClient;
using Mysqlx.Crud;
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
						throw new Exception("Error getting user.", ex);
					}
				}
			}
		}

        public int GetPoints(string id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT points FROM [user] WHERE id = @UserId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@UserId", id);

				        connection.Open();
				        int points = Convert.ToInt32(command.ExecuteScalar());

				        return points;
			        }
			        catch (Exception ex)
			        {
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting points of user.", ex);
					}
		        }
	        }
        }

        public List<int> GetVotedActivities(string id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
				List<int> activityId = new List<int>();
		        string query = "SELECT activity_id FROM activity_user WHERE voted_user_id = @UserId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@UserId", id);

				        connection.Open();

				        using (SqlDataReader reader = command.ExecuteReader())
				        {
					        while (reader.Read())
					        {
						        activityId.Add(reader.GetInt32(0));
					        }
				        }

				        return activityId;
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting activities the user voted on.", ex);
			        }
		        }
	        }
		}
        public int getVoteId(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT id FROM activity_user WHERE voted_user_id = @UserId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@UserId", id);

				        connection.Open();

				        using (SqlDataReader reader = command.ExecuteReader())
				        {
					        while (reader.Read())
					        {
								return reader.GetInt32(0);
							}
						}
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting vote id.", ex);
			        }
		        }
	        }
		}
	}
}
