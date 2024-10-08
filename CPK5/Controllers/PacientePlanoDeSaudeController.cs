using CKP4.Data;
using CKP4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CKP4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientePlanoDeSaudeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientePlanoDeSaudeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("associar")]
        public async Task<IActionResult> AssociarPacientePlanoSaudeAsync([FromQuery] int pacienteId, [FromQuery] int planoSaudeId)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            var planoSaude = await _context.PlanosSaude.FindAsync(planoSaudeId);

            if (paciente == null || planoSaude == null)
            {
                return NotFound("Paciente ou Plano de Saúde não encontrado.");
            }

            var associacaoExistente = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacaoExistente == null)
            {
                _context.PacientePlanosSaude.Add(new PacientePlanoSaude { PacienteId = pacienteId, PlanoSaudeId = planoSaudeId });
                await _context.SaveChangesAsync();
            }

            return Ok("Paciente associado ao Plano de Saúde com sucesso.");
        }



        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverPacientePlanoSaudeAsync([FromQuery] int pacienteId, [FromQuery] int planoSaudeId)
        {
            var associacao = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacao == null)
            {
                return NotFound("Associação não encontrada.");
            }

            _context.PacientePlanosSaude.Remove(associacao);
            await _context.SaveChangesAsync();

            return Ok("Associação entre Paciente e Plano de Saúde removida com sucesso.");
        }


        [HttpGet("planos-do-paciente")]
        public async Task<IActionResult> ListarPlanosDeSaudeAssociadosAsync([FromQuery] int pacienteId)
        {
            var planosDeSaude = await _context.PacientePlanosSaude
                .Where(pp => pp.PacienteId == pacienteId)
                .Select(pp => pp.PlanoSaude)
                .ToListAsync();

            if (planosDeSaude == null || !planosDeSaude.Any())
            {
                return NotFound("Paciente não possui planos de saúde associados.");
            }

            return Ok(planosDeSaude);
        }


        [HttpGet("pacientes-do-plano")]
        public async Task<IActionResult> ListarPacientesAssociadosAsync([FromQuery] int planoSaudeId)
        {
            var pacientes = await _context.PacientePlanosSaude
                .Where(pp => pp.PlanoSaudeId == planoSaudeId)
                .Select(pp => pp.Paciente)
                .ToListAsync();

            if (pacientes == null || !pacientes.Any())
            {
                return NotFound("Plano de Saúde não possui pacientes associados.");
            }

            return Ok(pacientes);
        }
    }
}