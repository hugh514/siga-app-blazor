
namespace SigaApp.Models.Professor
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; } 
        public string Telefone { get; set; } 
        public string Status { get; set; } = "Ativo";
        public string Disciplina { get; set; } 
        public string Especialidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public string TurmasVinculadas { get; set; }
    }
}
