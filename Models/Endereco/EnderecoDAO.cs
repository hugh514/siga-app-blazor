using MySql.Data.MySqlClient;
using SigaApp.Configs;

namespace SigaApp.Models.Endereco
{
    public class EnderecoDAO
    {
        private readonly Conexao _conexao;

        public EnderecoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Excluir(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM endereco WHERE id_end = @_id";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
