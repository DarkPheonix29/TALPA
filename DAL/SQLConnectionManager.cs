using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public static class SQLConnectionManager
    {
        private static string? _connectionString;

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
