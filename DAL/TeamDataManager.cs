using Microsoft.Data.SqlClient;

namespace DAL
{
	public class TeamDataManager
	{
		public void CreateTeam( string name)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO team (name) VALUES (@Name)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Name", name);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error creating team.", ex);
					}
				}
			}
		}

		public bool TeamExists(string name)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT CASE WHEN EXISTS ( SELECT * FROM [team] WHERE name = @Name ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Name", name);

						connection.Open();
						bool existence = Convert.ToBoolean(command.ExecuteScalar());

						return existence;
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error checking if team exists.", ex);
					}
				}
			}
		}

		public void AddMemberToTeam(string userId, int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"UPDATE [user] SET team_id = @TeamId WHERE id = @id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", teamId);
						command.Parameters.AddWithValue("@id", userId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error adding user to team.", ex);
					}
				}
			}
		}

		public void RemoveMemberFromTeam(string userId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"UPDATE [user] SET team_id = null WHERE id = @Id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Id", userId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error removing user from team.", ex);
					}
				}
			}
		}

		public void RemoveAllMembersFromTeam(int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"UPDATE [user] SET team_id = null WHERE team_id = @Id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Id", teamId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error removing all users from team.", ex);
					}
				}
			}
		}

		public void DeleteTeam(int teamId)
		{
			RemoveAllMembersFromTeam(teamId);
			PollDataManager pdm = new();
			pdm.DeletePoll(teamId);
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"DELETE FROM team WHERE id = @Id";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@Id", teamId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error adding user to team.", ex);
					}
				}
			}
		}

		public int GetTeamId(string TeamName)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT id FROM team WHERE name = @name";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@name", TeamName);

						connection.Open();
						int id = Convert.ToInt32(command.ExecuteScalar());
						return id;
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting team id.", ex);
					}
				}
			}
		}

		public bool CheckPlannedActivity(int TeamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT 1 FROM team_activity WHERE teamId = @teamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@teamId", TeamId);

						connection.Open();
						int check = Convert.ToInt32(command.ExecuteScalar());
						if (check == 0)
						{
							return false;
						}
						else
						{
							return true;
						}
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error checking team activity.", ex);
					}
				}
			}
		}

		public int getPlannedActivityId(int TeamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT activityId FROM team_activity WHERE teamId = @teamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@teamId", TeamId);

						connection.Open();
						int teamId = Convert.ToInt32(command.ExecuteScalar());
						return teamId;
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error checking team activity.", ex);
					}
				}
			}
		}
	}
}
