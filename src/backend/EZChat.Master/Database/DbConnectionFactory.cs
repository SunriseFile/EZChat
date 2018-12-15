using System.Data;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Npgsql;

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
            var connection = new NpgsqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}
