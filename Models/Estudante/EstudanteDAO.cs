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

                string sql = @"
            SELECT 
                estudante.id_est,
                estudante.nome_est,
                estudante.idade_est,
                estudante.data_nasc_est,
                estudante.telefone_est,
                estudante.nome_mae_est,
                estudante.nome_pai_ou_resp_est,
                estudante.situacao_est,
                estudante.sexo_est,
                estudante.id_end_fk,
                estudante.id_tur_fk,

                endereco.id_end,
                endereco.cidade_end,
                endereco.uf_end,
                endereco.rua_end,
                endereco.numero_end,
                endereco.bairro_end,

                turma.id_tur,
                turma.nome_tur,
                turma.ano_tur,
                turma.turno_tur,
                turma.capacidade_maxima_tur

                FROM estudante 
                LEFT JOIN endereco ON (estudante.id_end_fk = endereco.id_end)
                LEFT JOIN turma ON (estudante.id_tur_fk = turma.id_tur);
        ";

                using (var comando = new MySqlCommand(sql, conn))
                using (var leitor = comando.ExecuteReader())
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
                            NomeMae = leitor.GetString("nome_mae_est"),
                            NomeResp = leitor.GetString("nome_pai_ou_resp_est"),
                            Situacao = leitor.GetString("situacao_est"),
                            Sexo = leitor.GetString("sexo_est"),
                            Id_End = leitor.GetInt32("id_end_fk"),
                            Id_Tur = leitor.GetInt32("id_tur_fk"),
                            
                            Endereco = new Endereco
                            {
                                Id = leitor.GetInt32("id_end"),
                                Cidade = leitor.GetString("cidade_end"),
                                Uf = leitor.GetString("uf_end"),
                                Rua = leitor.GetString("rua_end"),
                                Numero = leitor.GetString("numero_end"),
                                Bairro = leitor.GetString("bairro_end")
                            },
                            
                            Turma = new Turma.Turma
                            {
                                Id = leitor.GetInt32("id_tur"),
                                Nome = leitor.GetString("nome_tur"),
                                Ano = leitor.GetString("ano_tur"),
                                Turno = leitor.GetString("turno_tur"),
                                Capacidade = leitor.GetInt32("capacidade_maxima_tur")
                            }
                        };

                        lista.Add(estudante);
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
    }

  
}
