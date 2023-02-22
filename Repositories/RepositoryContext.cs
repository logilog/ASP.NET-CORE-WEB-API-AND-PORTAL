using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Repositories
{
    public class RepositoryContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public RepositoryContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);
    }

}
