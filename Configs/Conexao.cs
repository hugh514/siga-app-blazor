using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace SigaApp.Configs
{
    public class Conexao
    {
        private readonly string _connectionString;

        public Conexao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection") ?? "";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
