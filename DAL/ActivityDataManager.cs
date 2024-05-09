using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ActivityDataManager
    {
        public void ActivitySubmit(string name, string description, string dateAdded, List<string> limitations, List<string> categories, string proposingUserId, string location)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity (name, description, proposing_user, date_added, location) OUTPUT INSERTED.id VALUES (@Name, @Description, @Proposing_user, @Date_added, @Location)";
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Proposing_user", proposingUserId);
                        command.Parameters.AddWithValue("@Date_added", dateAdded);
                        command.Parameters.AddWithValue("@Location", location);

                        connection.Open();

                        int activityId = Convert.ToInt32(command.ExecuteScalar());

                        Add_limitations(limitations, activityId);
						Add_categories(categories, activityId);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error submitting activity.", ex);
                    }
                }
            }
        }

        public void Add_limitations(List<string> limitations, int activityId)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity_limitation (activity_id, limitation) VALUES (@ActivityId, @Limitation)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
					connection.Open();
					foreach (string limitation in limitations)
                    {
                        try
                        {
	                        command.Parameters.Clear();
							command.Parameters.AddWithValue("@ActivityId", activityId);
                            command.Parameters.AddWithValue("@Limitation", limitation);

                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions appropriately (e.g., logging)
                            throw new Exception("Error adding limitation.", ex);
                        }
                    }
                }
            }
        }

        public void Add_categories(List<string> categories, int activityId)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = $"INSERT INTO activity_category (activity_id, category) VALUES (@ActivityId, @Category)";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        connection.Open();
			        foreach (string category in categories)
			        {
				        try
				        {
					        command.Parameters.Clear();
					        command.Parameters.AddWithValue("@ActivityId", activityId);
					        command.Parameters.AddWithValue("@Category", category);

					        command.ExecuteNonQuery();
				        }
				        catch (Exception ex)
				        {
					        // Handle exceptions appropriately (e.g., logging)
					        throw new Exception("Error adding categories.", ex);
				        }
			        }
		        }
	        }
		}
        public void Add_dates(List<DateTime> dates, int activityId)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = $"INSERT INTO activity_date (activity_id, date) VALUES (@ActivityId, @date)";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        connection.Open();
			        foreach (DateTime date in dates)
			        {
				        try
				        {
					        command.Parameters.Clear();
					        command.Parameters.AddWithValue("@ActivityId", activityId);
					        command.Parameters.AddWithValue("@date", date);

					        command.ExecuteNonQuery();
				        }
				        catch (Exception ex)
				        {
					        // Handle exceptions appropriately (e.g., logging)
					        throw new Exception("Error adding dates.", ex);
				        }
			        }
		        }
	        }
        }

		public void VotedUserUpdate(string votedUserId, int activityId)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity_user (activity_id, voted_user_id) VALUES (@ActivityId, @VotedUserId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ActivityId", activityId);
                        command.Parameters.AddWithValue("@VotedUserId", votedUserId);

						connection.Open();

						command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error updating voted user.", ex);
                    }
                }
            }
        }
        public DataRow GetActivity(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM Activity WHERE id = @ActivityId";
				using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
                        command.Parameters.AddWithValue("@ActivityId", id);

				        connection.Open();
						DataTable dt = new();
						using (SqlDataAdapter da = new(command))
				        {
					        da.Fill(dt);
				        }

						DataRow dr = dt.Rows[0];

						return dr;
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting activity.", ex);
			        }
				}
	        }
        }

        public DataTable GetLimitations(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM activity_limitation WHERE activity_id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

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
				        throw new Exception("Error getting limitation.", ex);
			        }
		        }
	        }
		}

        public DataTable GetAllUniqueLimitations()
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT DISTINCT limitation FROM activity_limitation";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {

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
				        throw new Exception("Error getting unique limitations.", ex);
			        }
		        }
	        }
        }

		public DataTable GetCategories(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM activity_category WHERE activity_id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

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
				        throw new Exception("Error getting categories.", ex);
			        }
		        }
	        }
		}

        public DataTable GetAllUniqueCategories()
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT DISTINCT category FROM activity_category";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {

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
				        throw new Exception("Error getting unique categories.", ex);
			        }
		        }
	        }
		}

        public void DeleteActivityById(int id)
        {
			RemoveDates(id);
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "DELETE FROM Activity WHERE id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

				        connection.Open();

						command.ExecuteNonQuery();
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error deleting activity.", ex);
			        }
		        }
	        }
		}

        public DataTable GetAllActivity()
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM activity";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
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
				        throw new Exception("Error getting activity's.", ex);
			        }
		        }
	        }
		}

        public DataTable GetActivityFromUser(string userId)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM activity WHERE proposing_user = @ProposingUser";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ProposingUser", userId);

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
				        throw new Exception("Error getting activity's of user.", ex);
			        }
		        }
	        }
		}

        public void ChooseActivity(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "INSERT INTO activity (has_been_chosen) VALUES (@HasBeenChosen) WHERE id = @id";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@HasBeenChosen", 1);
				        command.Parameters.AddWithValue("@id", id);
						connection.Open();
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error choosing activity.", ex);
			        }
		        }
	        }
		}

        public DataTable GetDates(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "SELECT * FROM activity_dates WHERE activity_id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

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
				        throw new Exception("Error getting dates.", ex);
			        }
		        }
	        }
		}

        public void RemoveDates(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = "DELETE FROM Activity_date WHERE activity_id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

				        connection.Open();

						command.ExecuteNonQuery();
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error removing dates.", ex);
			        }
		        }
	        }
		}

        public void SubmitLimitation(string description, string type)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        string query = $"INSERT INTO limitation (description, type) OUTPUT INSERTED.id VALUES (@Description, @Type)";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@Type", type);
				        command.Parameters.AddWithValue("@Description", description);

				        connection.Open();

				        command.ExecuteNonQuery();
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error submitting activity.", ex);
			        }
		        }
	        }
		}

        public List<string> GetVotedUsers(int id)
        {
	        using (var connection = ConnectionManager.GetConnection() as SqlConnection)
	        {
		        List<string> UserId = new List<string>();
		        string query = "SELECT voted_user_id FROM activity_user WHERE activity_id = @ActivityId";
		        using (SqlCommand command = new SqlCommand(query, connection))
		        {
			        try
			        {
				        command.Parameters.AddWithValue("@ActivityId", id);

				        connection.Open();

				        using (SqlDataReader reader = command.ExecuteReader())
				        {
					        while (reader.Read())
					        {
						        UserId.Add(reader.GetString(0));
					        }
				        }

				        return UserId;
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error getting activities the user voted on.", ex);
			        }
		        }
	        }
		}
	}
}
