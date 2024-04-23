using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	internal class PollDataManager1
	{
		public void PollSubmit(string ManagerId, DateTime deadline, List<int> activitys)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO poll (manager_id, deadline) VALUES (@ManagerId, @Deadline)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ManagerId", ManagerId);
						command.Parameters.AddWithValue("@Deadline", deadline);
						CreateSelection(activitys, ManagerId);


						connection.Open();

						command.ExecuteNonQuery();

					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error submitting poll.", ex);
					}
				}
			}
		}

		public void CreateSelection(List<int> activityId, string ManagerId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = $"INSERT INTO poll_activity (activity_id, manager_id) VALUES (@ActivityId, @ManagerId)";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					foreach (int id in activityId)
					{
						try
						{
							command.Parameters.Clear();
							command.Parameters.AddWithValue("@ActivityId", id);
							command.Parameters.AddWithValue("@ManagerId", ManagerId);

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

		public void DeletePoll(int managerId)
		{
			using (var connection = ConnectionManager.GetConnection() as SqlConnection)
			{
				string query = "DELETE * FROM Poll WHERE manager_id = @ManagerId";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					try
					{
						command.Parameters.AddWithValue("@ManagerId", managerId);

						connection.Open();
					}
					catch (Exception ex)
					{
						// Handle exceptions appropriately (e.g., logging)
						throw new Exception("Error deleting poll.", ex);
					}
				}
			}
		}
	}
}
