using MySql.Data.MySqlClient;
using SigaApp.Configs;

namespace SigaApp.Models.Turma
{
    public class TurmaDAO
    {
        private readonly Conexao _conexao;

        public TurmaDAO(Conexao conexao)
        {
            _conexao = conexao;
        }
        // 🔹 Listar todos os professores
        public List<Turma> ListarTodos()
        {
            var lista = new List<Turma>();

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "SELECT * FROM turma";

                using (var comando = new MySqlCommand(sql, conn))
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        var turma = new Turma
                        {
                            Id = leitor.GetInt32("id_tur"),
                            Nome = leitor.GetString("nome_tur"),
                            Ano = leitor.GetString("ano_tur"),                            
                            Turno = leitor.GetString("turno_tur"),
                            Capacidade =leitor.GetInt32("capacidade_maxima_tur")

                        };

                        lista.Add(turma);
                    }
                }
            }

            return lista;
        }
    }
}
