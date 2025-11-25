namespace SigaApp.Models.Estudante
{
    public class Estudante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNasc { get; set; }
        public string Telefone { get; set; }
        public string NomeResp1 { get; set; }
        public string NomeResp2 { get; set; }
        public string Situacao { get; set; } = "cursando"; 
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }

        public int Id_Tur { get; set; }
        public Turma.Turma Turma { get; set; }

    }
}
