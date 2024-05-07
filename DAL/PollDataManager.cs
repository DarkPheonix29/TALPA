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
		public void PollSubmit(string TeamId, DateTime deadline, List<int> activitys)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO poll (team_id, deadline) OUTPUT INSERTED.id VALUES (@TeamId, @Deadline)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", TeamId);
						command.Parameters.AddWithValue("@Deadline", deadline);

						connection.Open();

						int PollId = Convert.ToInt32(command.ExecuteScalar());
						CreateSelection(activitys, PollId);

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
				string query = $"INSERT INTO poll_activity (activity_id, poll_id) VALUES (@ActivityId, @PollId)";
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

		public void UpdateVotes(int activityId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"UPATE poll_activity SET votes = votes + 1 WHERE activity = @ActivityId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ActivityId", activityId);

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
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "DELETE * FROM Poll WHERE team_id = @TeamId";
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

		public DataRow GetPollOfTeam(int teamId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT id FROM poll WHERE team_id = @TeamId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@TeamId", teamId);

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
						throw new Exception("Error getting poll id.", ex);
					}
				}
			}
		}

		public DataTable GetSelection(int pollId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "SELECT activity_id, activity_votes FROM activity_poll WHERE poll_id = @PollId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@PollId", pollId);

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
						throw new Exception("Error getting selection.", ex);
					}
				}
			}
		}
	}
}
