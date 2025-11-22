using MySql.Data.MySqlClient;
using SigaApp.Configs;

namespace SigaApp.Models.Estudante
{
    public class EstudanteDAO
    {
        private readonly Conexao _conexao;

        public EstudanteDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // 🔹 Listar todos os estudantes
        public List<Estudante> ListarTodos()
        {
            var lista = new List<Estudante>();

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                // -----------------------------------------
                // 1) BUSCAR ESTUDANTE + ENDEREÇO
                // -----------------------------------------
                string sql = @"
            SELECT 
                est.id_est,
                est.nome_est,
                est.idade_est,
                est.data_nasc_est,
                est.telefone_est,
                est.nome_resp_1,
                est.nome_resp_2,
                est.situacao_est,
                est.sexo_est,
                est.id_tur_fk,

                ender.id_end,
                ender.cidade_end,
                ender.uf_end,
                ender.rua_end,
                ender.numero_end,
                ender.bairro_end

            FROM estudante est
            LEFT JOIN endereco ender
                ON est.id_end_fk = ender.id_end";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var leitor = cmd.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        var estudante = new Estudante
                        {
                            Id = leitor.GetInt32("id_est"),
                            Nome = leitor.GetString("nome_est"),
                            Idade = leitor.GetInt32("idade_est"),
                            DataNasc = leitor.GetDateTime("data_nasc_est"),
                            Telefone = leitor.GetString("telefone_est"),
                            NomeResp1 = leitor.GetString("nome_resp_1"),
                            NomeResp2 = leitor.GetString("nome_resp_2"),
                            Situacao = leitor.GetString("situacao_est").ToLower(),
                            Sexo = leitor.GetString("sexo_est").ToLower(),

                            Id_Tur = leitor.IsDBNull(leitor.GetOrdinal("id_tur_fk"))
                                        ? 0
                                        : leitor.GetInt32("id_tur_fk"),

                            // ENDEREÇO
                            Endereco = new Endereco.Endereco
                                {
                                    Id = leitor.GetInt32("id_end"),
                                    Cidade =  leitor.GetString("cidade_end"),
                                    Uf = leitor.GetString("uf_end"),
                                    Rua = leitor.GetString("rua_end"),
                                    Numero = leitor.GetString("numero_end"),
                                    Bairro = leitor.GetString("bairro_end")
                                }
                        };

                        lista.Add(estudante);
                    }
                }

                // -----------------------------------------
                // 2) BUSCAR TURMA SEPARADAMENTE
                // -----------------------------------------
                foreach (var est in lista)
                {
                    if (est.Id_Tur == 0)
                        continue;

                    string sqlTurma = @"
                SELECT 
                    id_tur,
                    nome_tur,
                    ano_tur,
                    turno_tur,
                    capacidade_maxima_tur
                FROM turma
                WHERE id_tur = @id";

                    using (var cmdTur = new MySqlCommand(sqlTurma, conn))
                    {
                        cmdTur.Parameters.AddWithValue("@id", est.Id_Tur);

                        using (var leitorTur = cmdTur.ExecuteReader())
                        {
                            if (leitorTur.Read())
                            {
                                est.Turma = new Turma.Turma
                                {
                                    Id = leitorTur.GetInt32("id_tur"),
                                    Nome = leitorTur.GetString("nome_tur"),
                                    Ano = leitorTur.GetString("ano_tur"),
                                    Turno =  leitorTur.GetString("turno_tur"),
                                    Capacidade = leitorTur.GetInt32("capacidade_maxima_tur")
                                };
                            }
                        }
                    }
                }
            }

            return lista;
        }


         
        public Estudante BuscarPorId(int id)
        {
            try
            {
                return ListarTodos().FirstOrDefault(e => e.Id == id)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Estudante();
            }
           

            
        }

        public void Inserir(Estudante novo)
        {
            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        
                        string sqlEnd = @"
                    INSERT INTO endereco(cidade_end, uf_end, rua_end, numero_end, bairro_end)
                    VALUES (@Cidade, @Uf, @Rua, @Numero, @Bairro);
                    SELECT LAST_INSERT_ID();
                ";

                        int idEndereco;

                        using (var cmdEnd = new MySqlCommand(sqlEnd, conn, trans))
                        {
                            cmdEnd.Parameters.AddWithValue("@Cidade", novo.Endereco.Cidade);
                            cmdEnd.Parameters.AddWithValue("@Uf", novo.Endereco.Uf);
                            cmdEnd.Parameters.AddWithValue("@Rua", novo.Endereco.Rua);
                            cmdEnd.Parameters.AddWithValue("@Numero", novo.Endereco.Numero);
                            cmdEnd.Parameters.AddWithValue("@Bairro", novo.Endereco.Bairro);

                            idEndereco = Convert.ToInt32(cmdEnd.ExecuteScalar());
                        }

                       
                        string sqlEst = @"
                    insert into estudante
                    (nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, 
                     nome_resp_1, nome_resp_2, situacao_est, id_end_fk, id_tur_fk)
                    VALUES
                    (@_nome, @_idade, @_sexo, @_dataNasc, @_telefone,
                     @_resp, @_mae, @_situacao, @_idEnd, null);
                ";

                        using (var cmdEst = new MySqlCommand(sqlEst, conn, trans))
                        {
                            cmdEst.Parameters.AddWithValue("@_nome", novo.Nome);
                            cmdEst.Parameters.AddWithValue("@_idade", novo.Idade);
                            cmdEst.Parameters.AddWithValue("@_sexo", novo.Sexo.ToLower());
                            cmdEst.Parameters.AddWithValue("@_dataNasc", novo.DataNasc);
                            cmdEst.Parameters.AddWithValue("@_telefone", novo.Telefone);
                            cmdEst.Parameters.AddWithValue("@_resp", novo.NomeResp1);
                            cmdEst.Parameters.AddWithValue("@_mae", novo.NomeResp2);
                            cmdEst.Parameters.AddWithValue("@_situacao", novo.Situacao.ToLower());
                            cmdEst.Parameters.AddWithValue("@_idEnd", idEndereco);

                            

                            cmdEst.ExecuteNonQuery();
                        }

                        
                        trans.Commit();
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        trans.Rollback();                        
                    }
                }

                
            }
            
        }
        public void Excluir(int id)
        {

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM estudante WHERE id_est = @_id";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_id", id);
                    comando.ExecuteNonQuery();
                }
            }
        }

    }


}
