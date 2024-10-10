using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CPK5.Models
{
    [Table("TB_CP5_PACIENTE")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DtNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Telefone { get; set; }

        public ICollection<PacientePlanoSaude>? PacientePlanosSaude { get; set; }


        public Paciente() { }
        public Paciente(int id, string nome, DateTime dtNascimento, string cPF, string endereco, string telefone)
        {
            Id = id;
            Nome = nome;
            DtNascimento = dtNascimento;
            CPF = cPF;
            Endereco = endereco;
            Telefone = telefone;
        }
    }
}
