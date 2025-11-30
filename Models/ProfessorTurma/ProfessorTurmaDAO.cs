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

            return lista;
        }
    }
}
