using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SubmitActivity
    {
        public static void ActivitySubmit(string name, string description, DateTime dateAdded, List<string> limitations, int proposingUserId, List<int> votedusersId)
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                string query = $"INSERT INTO activity (name, description, proposing_user, date_added) VALUES (@Name, @Description, @Proposing_user, @Date_added)";
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Data", name);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Proposing_user", proposingUserId);
                        command.Parameters.AddWithValue("@Date_added", dateAdded);

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
    }
}
