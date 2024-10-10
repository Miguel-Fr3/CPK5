using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPK5.Models
{
    [Table("TB_CP5_PLANO_SAUDE")]
    public class PlanoSaude
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NmPlano { get; set; }
        [Required]
        public string Cobertura { get; set; }

        public ICollection<PacientePlanoSaude>? PacientePlanosSaude { get; set; }

        public PlanoSaude() { }
        public PlanoSaude(int id, string nmPlano, string cobertura)
        {
            Id = id;
            NmPlano = nmPlano;
            Cobertura = cobertura;
        }
    }
}
