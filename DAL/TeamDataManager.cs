using Microsoft.Data.SqlClient;

namespace DAL
{
	public class TeamDataManager
	{
		public void CreateTeam(string managerId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO team (manager_id) VALUES (@managerId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ManagerId", managerId);

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
			//pdm.DeletePoll(teamId);
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
	}
}
