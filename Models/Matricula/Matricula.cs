namespace SigaApp.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public int Id_Est_Fk { get; set; }
        public int Id_Tur_Fk { get; set; }
    }
}
