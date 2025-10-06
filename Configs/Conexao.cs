using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;

namespace SigaApp.Configs
{
    public class Conexao
    {
        private readonly string _connectionString;

        public Conexao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection") ?? "";
        }

        // 🔹 Cria e retorna uma nova conexão pronta para uso (mas não aberta)
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // 🔹 Método auxiliar caso queira criar comandos rapidamente
        public MySqlCommand CreateCommand(string query)
        {
            var conn = GetConnection();
            conn.Open(); // abre para o comando
            return new MySqlCommand(query, conn);
        }
    }
}
