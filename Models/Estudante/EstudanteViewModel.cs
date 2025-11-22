using SigaApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SigaApp.Models.Estudante
{
    public class EstudanteViewModel
    {

        [Required(ErrorMessage = "O nome do estudante é obrigatório.")]
        [StringLength(300, ErrorMessage = "O nome deve ter no máximo 300 caracteres.")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(1, 120, ErrorMessage = "Idade inválida.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O sexo é obrigatório.")]
        public string Sexo { get; set; } = "";

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNasc { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; } = "";

        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [StringLength(300)]
        public string NomeResp1 { get; set; } = "";

        [StringLength(300)]
        public string NomeResp2 { get; set; } = "";

        [Required(ErrorMessage = "A situação do estudante é obrigatória.")]
        public string Situacao { get; set; } = "Cursando";

        // -----------------------------
        // Relações com outras tabelas
        // -----------------------------

        [Required]
        public EnderecoViewModel Endereco { get; set; }

        [Required]
        public TurmaViewModel Turma { get; set; }
    
    }
}

