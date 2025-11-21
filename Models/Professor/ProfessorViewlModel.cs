using System.ComponentModel.DataAnnotations;

namespace SigaApp.Models.Professor
{
    public class ProfessorViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14)]
        public string Cpf { get; set; } = "";

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string Telefone { get; set; } = "";

        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "";

        [Required(ErrorMessage = "A disciplina principal é obrigatória.")]
        public string DisciplinaPrincipal { get; set; } = "";

        [Required(ErrorMessage = "As turmas vinculadas são obrigatórias.")]
        public string TurmasVinculadas { get; set; } = "";

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        public string Especialidade { get; set; } = "";
    }
}
