using System.ComponentModel.DataAnnotations.Schema;

namespace CPK5.Models
{

    [Table("TB_CP5_PACIENTE_PLANO")]
    public class PacientePlanoSaude
    {
        public int PacienteId { get; set; }
        public int PlanoSaudeId { get; set; }
        public Paciente Paciente { get; set; }
        public PlanoSaude PlanoSaude { get; set; }


        public PacientePlanoSaude(int pacienteId, int planoSaudeId)
        {
            PacienteId = pacienteId;
            PlanoSaudeId = planoSaudeId;
        }
        public PacientePlanoSaude() { }
    }
}
