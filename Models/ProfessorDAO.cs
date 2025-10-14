using SigaApp.Configs;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SigaApp.Models
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
                    (nome_pro, cpf_pro, email_pro, telefone_pro, status_pro, 
                     disciplina_principal_pro, turmas_vinculadas_pro, especialidade_pro, data_cadastro_pro)
                    VALUES (@_nome, @_cpf, @_email, @_telefone, @_status, @_disciplina, @_turmas, @_especialidade, @_dataCadastro);
                ";
                
                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_nome", professor.Nome);
                    comando.Parameters.AddWithValue("@_cpf", professor.Cpf);
                    comando.Parameters.AddWithValue("@_email", professor.Email ?? "");
                    comando.Parameters.AddWithValue("@_telefone", professor.Telefone ?? "");
                    comando.Parameters.AddWithValue("@_status", professor.Status ?? "Ativo");
                    comando.Parameters.AddWithValue("@_disciplina", professor.DisciplinaPrincipal ?? "");
                    comando.Parameters.AddWithValue("@_turmas", professor.TurmasVinculadas ?? "");
                    comando.Parameters.AddWithValue("@_especialidade", professor.Especialidade ?? "");
                    comando.Parameters.AddWithValue("@_dataCadastro", professor.DataCadastro);

                    comando.ExecuteNonQuery();
                    Console.WriteLine("TEste");
                }
            }
        }

        // 🔹 Listar todos os professores
        public List<Professor> ListarTodos()
        {
            var lista = new List<Professor>();

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
                            Id = leitor.GetInt32("id_prof"),
                            Nome = leitor.GetString("nome_prof"),
                            Cpf = leitor.GetString("cpf_prof"),
                            Email = leitor.IsDBNull(leitor.GetOrdinal("email_prof")) ? "" : leitor.GetString("email_prof"),
                            Telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone_prof")) ? "" : leitor.GetString("telefone_prof"),
                            Status = leitor.IsDBNull(leitor.GetOrdinal("status_prof")) ? "Ativo" : leitor.GetString("status_prof"),
                            DisciplinaPrincipal = leitor.IsDBNull(leitor.GetOrdinal("disciplina_principal_prof")) ? "" : leitor.GetString("disciplina_principal_prof"),
                            TurmasVinculadas = leitor.IsDBNull(leitor.GetOrdinal("turmas_vinculadas_prof")) ? "" : leitor.GetString("turmas_vinculadas_prof"),
                            Especialidade = leitor.IsDBNull(leitor.GetOrdinal("especialidade_prof")) ? "" : leitor.GetString("especialidade_prof"),
                            DataCadastro = leitor.GetDateTime("data_cadastro_prof")
                        };

                        lista.Add(professor);
                    }
                }
            }

            return lista;
        }

        // 🔹 Excluir professor
        public void Excluir(int id)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM professor WHERE id_prof = @_id";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
    