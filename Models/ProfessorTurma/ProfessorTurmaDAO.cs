using MySql.Data.MySqlClient;
using SigaApp.Configs;

namespace SigaApp.Models
{
    public class ProfessorTurmaDAO
    {
        private readonly Conexao _conexao;

        public ProfessorTurmaDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        public List<ProfessorTurma> ListarPorTurma(int id)
        {
            List<ProfessorTurma> lista = new();

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT id_ptu, id_pro_fk, id_tur_fk
                       FROM professor_turma
                       WHERE id_tur_fk = @id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProfessorTurma p = new ProfessorTurma
                            {
                                Id = reader.GetInt32("id_ptu"),
                                Id_Professor_fk = reader["id_pro_fk"] == DBNull.Value ? null : reader.GetInt32("id_pro_fk"),
                                Id_Turma_fk = reader["id_tur_fk"] == DBNull.Value ? null : reader.GetInt32("id_tur_fk")
                            };

                            lista.Add(p);
                        }
                    }
                }
            }
            return lista;
        }

        public List<ProfessorTurma> ListarPorProfessor(int id)
        {
            List<ProfessorTurma> lista = new();

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT id_ptu, id_pro_fk, id_tur_fk
                       FROM professor_turma
                       WHERE id_pro_fk = @id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProfessorTurma p = new ProfessorTurma
                            {
                                Id = reader.GetInt32("id_ptu"),
                                Id_Professor_fk = reader["id_pro_fk"] == DBNull.Value ? null : reader.GetInt32("id_pro_fk"),
                                Id_Turma_fk = reader["id_tur_fk"] == DBNull.Value ? null : reader.GetInt32("id_tur_fk")
                            };

                            lista.Add(p);
                        }
                    }
                }
            }
            return lista;
        }
        // -------------------------------------------------------------
        // INSERIR VÍNCULO PROFESSOR → TURMA
        // -------------------------------------------------------------
        public void Inserir(ProfessorTurma vinculo)
        {
            using var conn = _conexao.GetConnection();
            conn.Open();

            string sql = @"
                INSERT INTO professor_turma (id_pro_fk, id_tur_fk)
                VALUES (@prof, @turma)";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@prof", vinculo.Id_Professor_fk);
            cmd.Parameters.AddWithValue("@turma", vinculo.Id_Turma_fk);

            cmd.ExecuteNonQuery();
        }


        // -------------------------------------------------------------
        // EXCLUIR VÍNCULO (caso queira remover depois)
        // -------------------------------------------------------------
        public void Excluir(int idProfessor, int idTurma)
        {
            using var conn = _conexao.GetConnection();
            conn.Open();

            string sql = @"
                DELETE FROM professor_turma
                WHERE id_pro_fk = @prof
                AND id_tur_fk = @turma";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@prof", idProfessor);
            cmd.Parameters.AddWithValue("@turma", idTurma);

            cmd.ExecuteNonQuery();
        }
    }
}
 