using System.ComponentModel.DataAnnotations;

namespace SigaApp.ViewModels
{
    public class EnderecoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(200, ErrorMessage = "A cidade deve ter no máximo 200 caracteres.")]
        public string Cidade { get; set; } = "";

        [Required(ErrorMessage = "O estado (UF) é obrigatório.")]
        [StringLength(2, ErrorMessage = "A UF deve ter exatamente 2 caracteres.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "A UF deve conter 2 letras maiúsculas (ex: SP, RJ).")]
        public string Uf { get; set; } = "";

        [Required(ErrorMessage = "A rua é obrigatória.")]
        [StringLength(200)]
        public string Rua { get; set; } = "";

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20)]
        public string Numero { get; set; } = "";

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(50)]
        public string Bairro { get; set; } = "";
    }
}