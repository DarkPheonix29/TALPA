using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ActivityDataManager
    {
        public static void ActivitySubmit(string name, string description, DateTime dateAdded, List<int> limitationsId, int proposingUserId)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity (name, description, proposing_user, date_added) OUTPUT INSERTED.id VALUES (@Name, @Description, @Proposing_user, @Date_added)";
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Proposing_user", proposingUserId);
                        command.Parameters.AddWithValue("@Date_added", dateAdded);

                        connection.Open();

                        int activityId = (int)command.ExecuteScalar();

                        Add_limitations(limitationsId, activityId);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions appropriately (e.g., logging)
                        throw new Exception("Error submitting activity.", ex);
                    }
                }
            }
        }

        public static void Add_limitations(List<int> limitationsId, int activityId)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity_limitation (activity_id, limitation_id) VALUES (@ActivityId, @LimitationId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
					connection.Open();
					foreach (int id in limitationsId)
                    {
                        try
                        {
	                        command.Parameters.Clear();
							command.Parameters.AddWithValue("@ActivityId", activityId);
                            command.Parameters.AddWithValue("@LimitationId", id);

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
        }

        public static void VotedUserUpdate(int votedUserId, int activityId)
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
                        throw new Exception("Error submitting activity.", ex);
                    }
                }
            }
        }
        public static DataTable GetActivity(int id)
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

				        return dt;
			        }
			        catch (Exception ex)
			        {
				        // Handle exceptions appropriately (e.g., logging)
				        throw new Exception("Error fetching activity.", ex);
			        }
				}
	        }
        }

        public static DataTable GetLimitations(int id)
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
				        throw new Exception("Error fetching activity.", ex);
			        }
		        }
	        }
		}
	}
}
