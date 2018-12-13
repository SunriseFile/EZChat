using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace EZChat.Master.Database
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IDbConnection> OpenAsync()
        {
            var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}
