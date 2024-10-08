using CKP5.Data;
using CKP5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace CKP5.Controllers
{
    public class PacientePlanoDeSaudeController : Controller
    {
        private readonly AppDbContext _context;

        public PacientePlanoDeSaudeController(AppDbContext context)
        {
            _context = context;
        }

        // Exibe uma view com os planos de saúde associados a um paciente
        public async Task<IActionResult> PlanosDoPaciente(int pacienteId)
        {
            var planosDeSaude = await _context.PacientePlanosSaude
                .Where(pp => pp.PacienteId == pacienteId)
                .Select(pp => pp.PlanoSaude)
                .ToListAsync();

            if (planosDeSaude == null || !planosDeSaude.Any())
            {
                ViewBag.Message = "Paciente não possui planos de saúde associados.";
                return View("Error");
            }

            return View(planosDeSaude); // Retorna uma view com os planos de saúde associados
        }

        // Exibe uma view com os pacientes associados a um plano de saúde
        public async Task<IActionResult> PacientesDoPlano(int planoSaudeId)
        {
            var pacientes = await _context.PacientePlanosSaude
                .Where(pp => pp.PlanoSaudeId == planoSaudeId)
                .Select(pp => pp.Paciente)
                .ToListAsync();

            if (pacientes == null || !pacientes.Any())
            {
                ViewBag.Message = "Plano de Saúde não possui pacientes associados.";
                return View("Error");
            }

            return View(pacientes); // Retorna uma view com os pacientes associados
        }

        // Formulário para associar um paciente a um plano de saúde
        [HttpGet]
        public IActionResult Associar()
        {
            // Carrega os dados necessários para o formulário
            ViewBag.Pacientes = _context.Pacientes.ToList();
            ViewBag.PlanosSaude = _context.PlanosSaude.ToList();
            return View();
        }

        // Processa o formulário de associação
        [HttpPost]
        public async Task<IActionResult> AssociarPacientePlanoSaude(int pacienteId, int planoSaudeId)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            var planoSaude = await _context.PlanosSaude.FindAsync(planoSaudeId);

            if (paciente == null || planoSaude == null)
            {
                ViewBag.Message = "Paciente ou Plano de Saúde não encontrado.";
                return View("Error");
            }

            var associacaoExistente = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacaoExistente == null)
            {
                _context.PacientePlanosSaude.Add(new PacientePlanoSaude { PacienteId = pacienteId, PlanoSaudeId = planoSaudeId });
                await _context.SaveChangesAsync();
            }

            ViewBag.Message = "Paciente associado ao Plano de Saúde com sucesso.";
            return RedirectToAction("PlanosDoPaciente", new { pacienteId });
        }

        // Formulário para remover a associação
        [HttpGet]
        public IActionResult Remover()
        {
            // Carrega os dados necessários para o formulário
            ViewBag.Pacientes = _context.Pacientes.ToList();
            ViewBag.PlanosSaude = _context.PlanosSaude.ToList();
            return View();
        }

        // Processa a remoção da associação
        [HttpPost]
        public async Task<IActionResult> RemoverPacientePlanoSaude(int pacienteId, int planoSaudeId)
        {
            var associacao = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacao == null)
            {
                ViewBag.Message = "Associação não encontrada.";
                return View("Error");
            }

            _context.PacientePlanosSaude.Remove(associacao);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Associação entre Paciente e Plano de Saúde removida com sucesso.";
            return RedirectToAction("PlanosDoPaciente", new { pacienteId });
        }
    }
}
