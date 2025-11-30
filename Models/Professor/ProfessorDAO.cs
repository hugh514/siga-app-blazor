using SigaApp.Configs;
using MySql.Data.MySqlClient;

namespace SigaApp.Models.Professor
{
    public class ProfessorDAO
    {
        private readonly Conexao _conexao;

        public ProfessorDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // 🔹 Inserir professor
        public void Inserir(Professor professor)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO professor 
                    (nome_pro, cpf_pro, email_pro, telefone_pro, disciplina_pro, 
                     status_pro, data_cadastro_pro, especialidade_pro)
                    VALUES (@_nome, @_cpf, @_email, @_telefone, @_disciplina, 
                            @_status, @_dataCadastro, @_especialidade);
                ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_nome", professor.Nome);
                    comando.Parameters.AddWithValue("@_cpf", professor.Cpf);
                    comando.Parameters.AddWithValue("@_email", professor.Email ?? "");
                    comando.Parameters.AddWithValue("@_telefone", professor.Telefone ?? "");
                    comando.Parameters.AddWithValue("@_disciplina", professor.Disciplina ?? "");
                    comando.Parameters.AddWithValue("@_status", professor.Status ?? "Ativo");
                    comando.Parameters.AddWithValue("@_dataCadastro", professor.DataCadastro);
                    comando.Parameters.AddWithValue("@_especialidade", professor.Especialidade ?? "");

                    comando.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Listar todos os professores
        public List<Professor> ListarTodos()
        {
            var lista = new List<Professor>();

            try
            {
                using (var conn = _conexao.GetConnection())
                {
                    conn.Open();

                    string sql = "SELECT * FROM professor";

                    using (var comando = new MySqlCommand(sql, conn))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            var professor = new Professor
                            {
                                Id = leitor.GetInt32("id_pro"),
                                Nome = leitor.GetString("nome_pro"),
                                Cpf = leitor.GetString("cpf_pro"),
                                Email = leitor.IsDBNull(leitor.GetOrdinal("email_pro")) ? "" : leitor.GetString("email_pro"),
                                Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_pro")) ? "" : leitor.GetString("telefone_pro"),
                                Disciplina = leitor.IsDBNull(leitor.GetOrdinal("disciplina_pro")) ? "" : leitor.GetString("disciplina_pro"),
                                Status = leitor.IsDBNull(leitor.GetOrdinal("status_pro")) ? "ativo" : leitor.GetString("status_pro"),
                                DataCadastro = leitor.IsDBNull(leitor.GetOrdinal("data_cadastro_pro"))
                                                ? DateTime.MinValue
                                                : leitor.GetDateTime("data_cadastro_pro"),
                                Especialidade = leitor.IsDBNull(leitor.GetOrdinal("especialidade_pro")) ? "" : leitor.GetString("especialidade_pro")
                            };

                            lista.Add(professor);
                        }
                    }

                }
                return lista;

            }
            catch
            (Exception ex) {
                return lista;
            }
           
           
        }

        // 🔹 Buscar por ID
        public Professor BuscarPorId(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "SELECT * FROM professor WHERE id_pro = @_id";
                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            return new Professor
                            {
                                Id = leitor.GetInt32("id_pro"),
                                Nome = leitor.GetString("nome_pro"),
                                Cpf = leitor.GetString("cpf_pro"),
                                Email = leitor.IsDBNull(leitor.GetOrdinal("email_pro")) ? "" : leitor.GetString("email_pro"),
                                Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_pro")) ? "" : leitor.GetString("telefone_pro"),
                                Disciplina = leitor.IsDBNull(leitor.GetOrdinal("disciplina_pro")) ? "" : leitor.GetString("disciplina_pro"),
                                Status = leitor.IsDBNull(leitor.GetOrdinal("status_pro")) ? "ativo" : leitor.GetString("status_pro"),
                                DataCadastro = leitor.IsDBNull(leitor.GetOrdinal("data_cadastro_pro"))
                                                ? DateTime.MinValue
                                                : leitor.GetDateTime("data_cadastro_pro"),
                                Especialidade = leitor.IsDBNull(leitor.GetOrdinal("especialidade_pro")) ? "" : leitor.GetString("especialidade_pro")
                            };
                        }
                    }
                }
            }

            return null;
        }

        // 🔹 Atualizar professor
        public void Atualizar(Professor professor)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"
                    UPDATE professor 
                    SET nome_pro = @_nome,
                        cpf_pro = @_cpf,
                        email_pro = @_email,
                        telefone_pro = @_telefone,
                        disciplina_pro = @_disciplina,
                        status_pro = @_status,
                        data_cadastro_pro = @_dataCadastro,
                        especialidade_pro = @_especialidade
                    WHERE id_pro = @_id;
                ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", professor.Id);
                    comando.Parameters.AddWithValue("@_nome", professor.Nome);
                    comando.Parameters.AddWithValue("@_cpf", professor.Cpf);
                    comando.Parameters.AddWithValue("@_email", professor.Email ?? "");
                    comando.Parameters.AddWithValue("@_telefone", professor.Telefone ?? "");
                    comando.Parameters.AddWithValue("@_disciplina", professor.Disciplina ?? "");
                    comando.Parameters.AddWithValue("@_status", professor.Status ?? "ativo");
                    comando.Parameters.AddWithValue("@_dataCadastro", professor.DataCadastro);
                    comando.Parameters.AddWithValue("@_especialidade", professor.Especialidade ?? "");

                    comando.ExecuteNonQuery();
                }
            }
        }

        // 🔹 Excluir professor
        public void Excluir(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM professor WHERE id_pro = @_id";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
