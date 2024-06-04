using Microsoft.Data.SqlClient;
using System.Data;
using DAL.Exceptions;

namespace DAL
{
	public class ActivityDataManager
	{
		public void ActivitySubmit(string name, string description, List<string> limitations, List<string> categories, string proposingUserId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query =
					$"INSERT INTO activity (name, description, proposing_user) OUTPUT INSERTED.id VALUES (@Name, @Description, @Proposing_user)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Name", name);
						command.Parameters.AddWithValue("@Description", description);
						command.Parameters.AddWithValue("@Proposing_user", proposingUserId);

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
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "DELETE FROM Team_activity WHERE activityId = @ActivityId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					//if id present in team_activity or activity_poll, do not delete

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

        public void DeleteSuggestionById(int id)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string checkQuery = "SELECT COUNT(*) FROM Team_activity WHERE activityId = @SuggestionId " +
                                    "UNION ALL " +
                                    "SELECT COUNT(*) FROM activity_poll WHERE activity_id = @SuggestionId";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@SuggestionId", id);

                    connection.Open();

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        string deleteQuery = "DELETE FROM activity WHERE id = @SuggestionId";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@SuggestionId", id);

                            try
                            {
                                deleteCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new SuggestionRemovalException($"Error removing suggestion with ID {id}:{ex.Message}");

							}
                        }
                    }
                    else
                    {
                        throw new SuggestionRemovalException($"Error removing suggestion with ID {id}: Suggestion in use");
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

		public void ChooseActivity(int id, string startDate, int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "UPDATE activity SET start_date = @startDate WHERE id = @id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@startDate", startDate);
						command.Parameters.AddWithValue("@id", id);

						AddActivityToTeamActivity(id, teamId);

						connection.Open();
						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error choosing activity.", ex);
					}
				}
			}
		}
		public List<string> GetVotedUsers(int id)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				List<string> UserId = new List<string>();
				string query = "SELECT voted_user_id FROM vote WHERE activity_id = @ActivityId";
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

		public string GetActivityCreator(int activityId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT proposing_user FROM activity WHERE id = @ActivityId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ActivityId", activityId);

						connection.Open();
						return command.ExecuteScalar().ToString();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting activity creator.", ex);
					}
				}
			}
		}

		public int GetTeamActivityId(int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT activityId FROM team_activity WHERE teamId = @teamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@teamId", teamId);

						connection.Open();
						return Convert.ToInt32(command.ExecuteScalar());
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting team activity id.", ex);
					}
				}
			}
		}

		public List<int> GetAllChosenActivityIds()
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				List<int> activityIds = new();
				string query = "SELECT activityId FROM team_activity";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								activityIds.Add(reader.GetInt32(0));
							}
						}

						return activityIds;
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting chosen activity ids.", ex);
					}
				}
			}
		}

		public void AddActivityToTeamActivity(int activityId, int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query =
					$"INSERT INTO team_activity (teamId, activityId) VALUES (@teamId, @activityId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@teamId", teamId);
						command.Parameters.AddWithValue("@activityId", activityId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error adding activity to team activity.", ex);
					}
				}
			}
		}

		public void AddLocationToActivity(int activityId, string location)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "UPDATE activity SET location = @location WHERE id = @id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@location", location);
						command.Parameters.AddWithValue("@id", activityId);

						connection.Open();
						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error adding location to activity.", ex);
					}
				}
			}
		}
	}
}
