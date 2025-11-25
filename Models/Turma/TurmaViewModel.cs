using System.ComponentModel.DataAnnotations;

namespace SigaApp.ViewModels
{
    public class TurmaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O ano da turma é obrigatório.")]
        [StringLength(100)]
        public string Ano { get; set; }

        [Required(ErrorMessage = "O turno é obrigatório.")]
        [StringLength(50)]
        public string Turno { get; set; }

        [Required(ErrorMessage = "Capacidade é obrigatório.")]
        [Range(1, 40, ErrorMessage = "A capacidade deve ser entre 1 e 40 alunos.")]
        public int Capacidade{ get; set; }

        [Required(ErrorMessage = "Periodo letivo é obrigatorio.")]
        [StringLength(50)]
        public string PeriodoLetivo { get; set; }

        [Required(ErrorMessage = "O status é obrigatorio.")]
        public string Status { get; set; } = "ativo";
       
    }
}
