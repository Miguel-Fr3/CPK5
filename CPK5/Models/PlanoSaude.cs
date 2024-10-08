using System.ComponentModel.DataAnnotations.Schema;

namespace CKP4.Models
{
    [Table("TB_CP4_PLANO_SAUDE")]
    public class PlanoSaude
    {
        public int Id { get; set; }
        public string NmPlano { get; set; }
        public int CdPlano { get; set; }
        public string Cobertura { get; set; }

        public ICollection<PacientePlanoSaude>? PacientePlanosSaude { get; set; }

        public PlanoSaude() { }
        public PlanoSaude(int id, string nmPlano, int cdPlano, string cobertura)
        {
            Id = id;
            NmPlano = nmPlano;
            CdPlano = cdPlano;
            Cobertura = cobertura;
        }
    }
}
