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
            SELECT * FROM estudante";


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
                            NomeResp1 = leitor.GetString("nome_resp_1_est"),
                            NomeResp2 = leitor.GetString("nome_resp_2_est"),
                            Situacao = leitor.GetString("situacao_est").ToLower(),
                            Sexo = leitor.GetString("sexo_est").ToLower(),
                            Cidade = leitor.GetString("cidade_est"),
                            Bairro = leitor.GetString("bairro_est"),
                            Numero = leitor.GetString("numero_est"),
                            Rua = leitor.GetString("rua_est"),
                            Uf = leitor.GetString("uf_est"),
                        };

                        lista.Add(estudante);
                    }
                };
            
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
            try
            {
                using (var conn = _conexao.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO estudante
                        (nome_est, idade_est, sexo_est, data_nasc_est, telefone_est, 
                        nome_resp_1_est, nome_resp_2_est, situacao_est, 
                        cidade_est, uf_est, rua_est, numero_est, bairro_est)
                        VALUES
                        (@_nome, @_idade, @_sexo, @_dataNasc, @_telefone,
                        @_resp1, @_resp2, @_situacao,
                        @_cidade, @_uf, @_rua, @_numero, @_bairro);
";

                    using (var comando = new MySqlCommand(sql, conn))
                    {
                        comando.Parameters.AddWithValue("@_nome", novo.Nome);
                        comando.Parameters.AddWithValue("@_idade", novo.Idade);
                        comando.Parameters.AddWithValue("@_sexo", novo.Sexo.ToLower());
                        comando.Parameters.AddWithValue("@_dataNasc", novo.DataNasc);
                        comando.Parameters.AddWithValue("@_telefone", novo.Telefone);
                        comando.Parameters.AddWithValue("@_resp1", novo.NomeResp1);
                        comando.Parameters.AddWithValue("@_resp2", novo.NomeResp2);
                        comando.Parameters.AddWithValue("@_situacao", novo.Situacao.ToLower());

                    
                        comando.Parameters.AddWithValue("@_cidade", novo.Cidade);
                        comando.Parameters.AddWithValue("@_uf", novo.Uf);
                        comando.Parameters.AddWithValue("@_rua", novo.Rua);
                        comando.Parameters.AddWithValue("@_numero", novo.Numero);
                        comando.Parameters.AddWithValue("@_bairro", novo.Bairro);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
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

        public void Atualizar(Estudante estudante)
        {

            using (var conn = _conexao.GetConnection())
            {
                conn.Open();
                string sql = @"
                UPDATE estudante
                SET nome_est = @_nome,
                idade_est = @_idade,
                sexo_est = @_sexo,
                data_nasc_est = @_dataNasc,
                telefone_est = @_telefone,
                nome_resp_1_est = @_resp,
                nome_resp_2_est = @_mae,
                situacao_est = @_situacao,
                cidade_est = @_cidade,
                uf_est = @_uf,
                rua_est = @_rua,
                numero_est = @_numero,
                bairro_est = @_bairro
                WHERE id_est = @_id;
                ";

                using (var comando = new MySqlCommand(sql, conn))
                {
                    comando.Parameters.AddWithValue("@_nome", estudante.Nome);
                    comando.Parameters.AddWithValue("@_idade", estudante.Idade);
                    comando.Parameters.AddWithValue("@_sexo", estudante.Sexo.ToLower());
                    comando.Parameters.AddWithValue("@_dataNasc", estudante.DataNasc);
                    comando.Parameters.AddWithValue("@_telefone", estudante.Telefone);
                    comando.Parameters.AddWithValue("@_resp", estudante.NomeResp1);
                    comando.Parameters.AddWithValue("@_mae", estudante.NomeResp2);
                    comando.Parameters.AddWithValue("@_situacao", estudante.Situacao.ToLower());

                    // novos campos
                    comando.Parameters.AddWithValue("@_cidade", estudante.Cidade);
                    comando.Parameters.AddWithValue("@_uf", estudante.Uf);
                    comando.Parameters.AddWithValue("@_rua", estudante.Rua);
                    comando.Parameters.AddWithValue("@_numero", estudante.Numero);
                    comando.Parameters.AddWithValue("@_bairro", estudante.Bairro);

                    // chave primária
                    comando.Parameters.AddWithValue("@_id", estudante.Id);

                    comando.ExecuteNonQuery();
                }

            }
        }
        

    }


}
