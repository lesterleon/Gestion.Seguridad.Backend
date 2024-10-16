using System.Data.SqlClient;

namespace Gestion.Seguridad.Backend.Infrastructure.Persistence.Configuration
{
    public static class SqlServerDatabase
    {
        public static string ConnectionString { get; set; } = string.Empty;
        private static string GetConnectionString()
        {
            return ConnectionString;
        }

        public static SqlConnection CreateSqlServerConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

    }
}
