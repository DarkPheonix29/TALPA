using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class PollDataManager
	{
		public void PollSubmit(int teamId, DateTime deadline, List<int> activitys)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO poll (team_id, deadline) OUTPUT INSERTED.id VALUES (@TeamId, @Deadline)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", teamId);
						command.Parameters.AddWithValue("@Deadline", deadline);

						connection.Open();

						int id = Convert.ToInt32(command.ExecuteScalar());
						CreateSelection(activitys, id);

					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error submitting poll.", ex);
					}
				}
			}
		}

		public void CreateSelection(List<int> activityId, int pollId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO activity_poll (activity_id, selection_id) VALUES (@ActivityId, @PollId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					foreach (int id in activityId)
					{
						try
						{
							command.Parameters.Clear();
							command.Parameters.AddWithValue("@ActivityId", id);
							command.Parameters.AddWithValue("@PollId", pollId);

							command.ExecuteNonQuery();

						}
						catch (Exception ex)
						{
							// Handle exceptions appropriately (e.g., logging)
							throw new Exception("Error creating selection.", ex);
						}
					}
				}
			}
		}

		public void UpdateVotes(int activityId, int pollId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"UPDATE activity_poll SET activity_votes = activity_votes + 1 WHERE activity_id = @ActivityId AND selection_id = @PollId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ActivityId", activityId);
						command.Parameters.AddWithValue("@PollId", pollId);


						connection.Open();

						command.ExecuteNonQuery();

					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error updating votes.", ex);
					}
				}
			}
		}

		public void DeletePoll(int teamId)
		{
			DeleteSelection(teamId);
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "DELETE FROM poll WHERE team_id = @TeamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", teamId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error deleting poll.", ex);
					}
				}
			}
		}

		public void DeleteSelection(int teamId)
		{
			int pollId = GetPollId(teamId);
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "DELETE FROM activity_poll WHERE selection_id = @PollId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@PollId", pollId);

						connection.Open();

						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error deleting selection.", ex);
					}
				}
			}
		}

		public int GetPollId(int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"SELECT (id) FROM poll WHERE team_id = @TeamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", teamId);

						connection.Open();

						int id = Convert.ToInt32(command.ExecuteScalar());

						return id;

					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error getting poll id.", ex);
					}
				}
			}
		}
	}
}
