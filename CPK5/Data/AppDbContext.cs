using Microsoft.EntityFrameworkCore;
using CPK5.Models;
namespace CPK5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<PlanoSaude> PlanosSaude { get; set; }
        public DbSet<PacientePlanoSaude> PacientePlanosSaude { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PacientePlanoSaude>()
                .HasKey(pp => new { pp.PacienteId, pp.PlanoSaudeId });


            modelBuilder.Entity<PlanoSaude>()
                .HasData(new List<PlanoSaude>
                {
                    new PlanoSaude(1, "Plano Inicial", "Cobertura Inicial")
                });

            modelBuilder.Entity<Paciente>()
                .HasData(new List<Paciente>
                {
                                new Paciente(1, "Paciente Inicial", DateTime.Now, "123456789", "endereço inicial", "123456789")
                });

            modelBuilder.Entity<PacientePlanoSaude>()
                .HasData(new List<PacientePlanoSaude>()
                {
                    new PacientePlanoSaude() { PacienteId = 1, PlanoSaudeId = 1 }, });





            base.OnModelCreating(modelBuilder);
        }


    }
}
