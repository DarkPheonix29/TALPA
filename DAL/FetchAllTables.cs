using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FetchAllTables
    {
        public static List<string> GetTableNames()
        {
            using (var connection = ConnectionManager.GetConnection() as SqlConnection)
            {
                try
                {
                    var tableNames = new List<string>();

					connection.Open();

					DataTable schema = connection.GetSchema("Tables");
                    foreach (DataRow row in schema.Rows)
                    {
                        tableNames.Add(row["TABLE_NAME"].ToString());
                    }

                    return tableNames;
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately (e.g., logging)
                    throw new Exception("Error fetching table names.", ex);
                }
            }
        }
    }
}
