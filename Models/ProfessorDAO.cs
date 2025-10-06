using SigaApp.Configs;
using SigaApp.Models;
using System;
using System.Collections.Generic;

namespace SigaApp.Models
{
    public class ProfessorDAO
    {
        private readonly Conexao _conexao;

        public ProfessorDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public void Inserir(Professor professor)
        {
            try
            {
                var comando = _conexao.CreateCommand(@"
                    INSERT INTO professor 
                    VALUES (null, @_nome, @_cpf, @_email, @_telefone, @_status, @_disciplina, @_turmas, @_especialidade, @_dataCadastro)
                ");

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
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Professor> ListarTodos()
        {
            var lista = new List<Professor>();

            var comando = _conexao.CreateCommand("SELECT * FROM professor");
            var leitor = comando.ExecuteReader();

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

            return lista;
        }
    }
}
