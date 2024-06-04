using Microsoft.Data.SqlClient;

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

        public bool UserExists(string id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT CASE WHEN EXISTS ( SELECT * FROM [user] WHERE id = @UserId ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@UserId", id);

				        connection.Open();
				        bool existence = Convert.ToBoolean(command.ExecuteScalar());

				        return existence;
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error checking if user exists.", ex);
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
		        string query = "SELECT activity_id FROM vote WHERE voted_user_id = @UserId";
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
        public int getVoteId(string id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT id FROM vote WHERE voted_user_id = @UserId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@UserId", id);

				        connection.Open();

				        return Convert.ToInt32(command.ExecuteScalar());
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting vote id.", ex);
			        }
		        }
	        }
		}

        public List<string> getVoteDates(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        List<string> dates = new();
		        string query = "SELECT date FROM vote_date WHERE vote_id = @VoteId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@VoteId", id);

				        connection.Open();

				        using (SqlDataReader reader = command.ExecuteReader())
				        {
					        while (reader.Read())
					        {
						        dates.Add(reader.GetString(0));
					        }
				        }

						return dates;
					}
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting vote id.", ex);
			        }
		        }
	        }
		}
        public void AddPointsToUser(string userId, int pointsToAdd)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = "UPDATE [user] SET points = points + @PointsToAdd WHERE id = @UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@PointsToAdd", pointsToAdd);
                        command.Parameters.AddWithValue("@UserId", userId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error updating user points.", ex);
                    }
                }
            }
        }
    }
}
