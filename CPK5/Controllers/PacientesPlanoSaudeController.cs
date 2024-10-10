using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CPK5.Data;
using CPK5.Models;

namespace CPK5.Controllers
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
            // Busca os planos associados ao paciente
            var planosDeSaude = await _context.PacientePlanosSaude
                .Where(pp => pp.PacienteId == pacienteId)
                .Select(pp => pp.PlanoSaude)
                .ToListAsync();

            if (!planosDeSaude.Any())
            {
                ViewBag.Message = "O paciente não possui planos de saúde associados.";
                return View("Error");
            }

            return View(planosDeSaude); // Retorna a view com os planos
        }

        // Exibe uma view com os pacientes associados a um plano de saúde
        public async Task<IActionResult> PacientesDoPlano(int planoSaudeId)
        {
            // Busca os pacientes associados ao plano de saúde
            var pacientes = await _context.PacientePlanosSaude
                .Where(pp => pp.PlanoSaudeId == planoSaudeId)
                .Select(pp => pp.Paciente)
                .ToListAsync();

            if (!pacientes.Any())
            {
                ViewBag.Message = "O plano de saúde não possui pacientes associados.";
                return View("Error");
            }

            return View(pacientes); // Retorna a view com os pacientes
        }

        // Exibe o formulário para associar um paciente a um plano de saúde
        [HttpGet]
        public IActionResult Associar()
        {
            // Carrega os pacientes e planos de saúde disponíveis para a associação
            ViewBag.Pacientes = _context.Pacientes.ToList();
            ViewBag.PlanosSaude = _context.PlanosSaude.ToList();
            return View(); // Renderiza a view Associar.cshtml
        }

        // Processa o formulário de associação entre paciente e plano de saúde
        [HttpPost]
        public async Task<IActionResult> AssociarPacientePlanoSaude(int pacienteId, int planoSaudeId)
        {
            // Verifica se os registros de paciente e plano de saúde existem
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            var planoSaude = await _context.PlanosSaude.FindAsync(planoSaudeId);

            if (paciente == null || planoSaude == null)
            {
                ViewBag.Message = "Paciente ou Plano de Saúde não encontrado.";
                return View("Error");
            }

            // Verifica se a associação já existe
            var associacaoExistente = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacaoExistente == null)
            {
                // Cria uma nova associação
                _context.PacientePlanosSaude.Add(new PacientePlanoSaude
                {
                    PacienteId = pacienteId,
                    PlanoSaudeId = planoSaudeId
                });
                await _context.SaveChangesAsync();
                ViewBag.Message = "Paciente associado ao Plano de Saúde com sucesso.";
            }
            else
            {
                ViewBag.Message = "Esta associação já existe.";
            }

            return RedirectToAction("PlanosDoPaciente", new { pacienteId });
        }

        // Exibe o formulário para remover a associação entre paciente e plano de saúde
        [HttpGet]
        public IActionResult Remover()
        {
            // Carrega os pacientes e planos de saúde para o formulário de remoção
            ViewBag.Pacientes = _context.Pacientes.ToList();
            ViewBag.PlanosSaude = _context.PlanosSaude.ToList();
            return View(); // Renderiza a view Remover.cshtml
        }

        // Processa a remoção da associação entre paciente e plano de saúde
        [HttpPost]
        public async Task<IActionResult> RemoverPacientePlanoSaude(int pacienteId, int planoSaudeId)
        {
            // Busca a associação para remoção
            var associacao = await _context.PacientePlanosSaude
                .FirstOrDefaultAsync(pp => pp.PacienteId == pacienteId && pp.PlanoSaudeId == planoSaudeId);

            if (associacao == null)
            {
                ViewBag.Message = "Associação não encontrada.";
                return RedirectToAction("Erro");
            }

            // Remove a associação
            _context.PacientePlanosSaude.Remove(associacao);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Associação removida com sucesso.";
            return RedirectToAction("Associar");

        }
    }
}