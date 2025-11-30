using Microsoft.AspNetCore.Http.HttpResults;

namespace SigaApp.Models
{
    public class ProfessorTurma
    {
        public int Id { get; set; }
        public int Id_Professor_fk { get; set; }
        public int Id_Turma_fk { get; set; }

    }
}