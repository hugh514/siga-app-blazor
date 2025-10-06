using System;

namespace SigaApp.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; } 
        public string Telefone { get; set; } 
        public string Status { get; set; } = "Ativo";
        public string DisciplinaPrincipal { get; set; } 
        public string TurmasVinculadas { get; set; }
        public string Especialidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
