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
		public void PollSubmit(int TeamId, string deadline, List<int> activitys)
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
				string query = $"INSERT INTO activity_poll (activity_id, poll_id) VALUES (@ActivityId, @PollId)";
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

		public void CreateVote(int activityId, string userId, List<string> dates)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO activity_user (activity_id, voted_user_id) OUTPUT INSERTED.id VALUES (@ActivityId, @UserId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ActivityId", activityId);
						command.Parameters.AddWithValue("@UserId", userId);

						connection.Open();

						int id = Convert.ToInt32(command.ExecuteScalar());
						addDatesToVote(dates, id);

					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error creating vote.", ex);
					}
				}
			}
		}

		public void addDatesToVote(List<string> dates, int voteId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO activity_user_date (date, vote_id) VALUES (@Date, @VoteId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					foreach (string date in dates)
					{
						try
						{
							command.Parameters.Clear();
							command.Parameters.AddWithValue("@Date", date);
							command.Parameters.AddWithValue("@VoteId", voteId);

							command.ExecuteNonQuery();

						}
						catch (Exception ex)
						{
							// Handle exceptions appropriately (e.g., logging)
							throw new Exception("Error adding dates to vote.", ex);
						}
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

		public List<string> getPollDates(int id)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				List<string> dates = new();
				string query = "SELECT date FROM poll_date WHERE poll_id = @PollId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@PollId", id);

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
	}
}
