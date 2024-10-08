using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CKP5.Data;
using CKP5.Models;

namespace CPK5.Controllers
{
    public class PlanosSaudeController : Controller
    {
        private readonly AppDbContext _context;

        public PlanosSaudeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlanosSaude
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanosSaude.ToListAsync());
        }

        // GET: PlanosSaude/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoSaude = await _context.PlanosSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoSaude == null)
            {
                return NotFound();
            }

            return View(planoSaude);
        }

        // GET: PlanosSaude/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanosSaude/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NmPlano,CdPlano,Cobertura")] PlanoSaude planoSaude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planoSaude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planoSaude);
        }

        // GET: PlanosSaude/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoSaude = await _context.PlanosSaude.FindAsync(id);
            if (planoSaude == null)
            {
                return NotFound();
            }
            return View(planoSaude);
        }

        // POST: PlanosSaude/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NmPlano,CdPlano,Cobertura")] PlanoSaude planoSaude)
        {
            if (id != planoSaude.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planoSaude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoSaudeExists(planoSaude.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(planoSaude);
        }

        // GET: PlanosSaude/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planoSaude = await _context.PlanosSaude
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planoSaude == null)
            {
                return NotFound();
            }

            return View(planoSaude);
        }

        // POST: PlanosSaude/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planoSaude = await _context.PlanosSaude.FindAsync(id);
            if (planoSaude != null)
            {
                _context.PlanosSaude.Remove(planoSaude);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoSaudeExists(int id)
        {
            return _context.PlanosSaude.Any(e => e.Id == id);
        }
    }
}
