using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public static class ConnectionManager
    {
        private static string? _connectionString;

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string has not been initialized.");
            }

            return new SqlConnection(_connectionString);
        }
    }
}
