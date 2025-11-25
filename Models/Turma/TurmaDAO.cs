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
                            PeriodoLetivo = leitor.GetString("periodo_letivo_tur"),
                            Status = leitor.GetString("status_tur").ToLower(),
                            Turno = leitor.GetString("turno_tur"),
                            Capacidade = leitor.GetInt32("capacidade_maxima_tur")

                        };

                        lista.Add(turma);
                    }
                }
            }

            return lista;
        }
        public void Inserir(Turma turma)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO turma 
                    (nome_tur, ano_tur, periodo_letivo_tur, turno_tur, 
                    status_tur, capacidade_maxima_tur)
                    VALUES 
                    (@_nome, @_ano, @_periodoLetivo, @_turno, @_status, @_capacidade);
                ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_nome", turma.Nome);
                    comando.Parameters.AddWithValue("@_ano", turma.Ano);
                    comando.Parameters.AddWithValue("@_periodoLetivo", turma.PeriodoLetivo ?? "");
                    comando.Parameters.AddWithValue("@_turno", turma.Turno);
                    comando.Parameters.AddWithValue("@_status", turma.Status ?? "ativo");
                    comando.Parameters.AddWithValue("@_capacidade", turma.Capacidade);

                    comando.ExecuteNonQuery();
                }
            }
        }
        public Turma? BuscarPorId(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT 
                id_tur,
                nome_tur,
                ano_tur,
                periodo_letivo_tur,
                turno_tur,
                status_tur,
                capacidade_maxima_tur
            FROM turma
            WHERE id_tur = @_id;
        ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            return new Turma
                            {
                                Id = leitor.GetInt32("id_tur"),
                                Nome = leitor.GetString("nome_tur"),
                                Ano = leitor.GetString("ano_tur"),
                                PeriodoLetivo = leitor.GetString("periodo_letivo_tur"),
                                Turno = leitor.GetString("turno_tur"),
                                Status = leitor.GetString("status_tur"),
                                Capacidade = leitor.GetInt32("capacidade_maxima_tur")
                            };
                        }
                    }
                }
            }

            return null;
        }
        public void Atualizar(Turma turma)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"
            UPDATE turma SET
                nome_tur = @_nome,
                ano_tur = @_ano,
                periodo_letivo_tur = @_periodoLetivo,
                turno_tur = @_turno,
                status_tur = @_status,
                capacidade_maxima_tur = @_capacidade
            WHERE id_tur = @_id;
        ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", turma.Id);
                    comando.Parameters.AddWithValue("@_nome", turma.Nome);
                    comando.Parameters.AddWithValue("@_ano", turma.Ano);
                    comando.Parameters.AddWithValue("@_periodoLetivo", turma.PeriodoLetivo);
                    comando.Parameters.AddWithValue("@_turno", turma.Turno);
                    comando.Parameters.AddWithValue("@_status", turma.Status);
                    comando.Parameters.AddWithValue("@_capacidade", turma.Capacidade);

                    comando.ExecuteNonQuery();
                }
            }
        }
        public void Excluir(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM turma WHERE id_tur = @_id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
