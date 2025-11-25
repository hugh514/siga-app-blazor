namespace SigaApp.Models.Turma
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ano { get; set; }
        public string PeriodoLetivo { get; set; }
        public string Turno { get; set; }
        public string Status { get; set; } = "ativo";
        public int Capacidade { get; set; }
        
    }
}
