using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prog5_3C2025.Data;
using Prog5_3C2025.Models;

namespace Prog5_3C2025.Controllers
{
    public class EstudianteUHsController : Controller
    {
        private readonly AppDbContext _context;

        public EstudianteUHsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstudianteUHs
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstudianteUH.ToListAsync());
        }

        // GET: EstudianteUHs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteUH = await _context.EstudianteUH
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteUH == null)
            {
                return NotFound();
            }

            return View(estudianteUH);
        }

        // GET: EstudianteUHs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstudianteUHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Edad")] EstudianteUH estudianteUH)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudianteUH);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Estudiante creado ok";
                return RedirectToAction(nameof(Index));
            }
            return View(estudianteUH);
        }

        // GET: EstudianteUHs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteUH = await _context.EstudianteUH.FindAsync(id);
            if (estudianteUH == null)
            {
                return NotFound();
            }
            return View(estudianteUH);
        }

        // POST: EstudianteUHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Edad")] EstudianteUH estudianteUH)
        {
            if (id != estudianteUH.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudianteUH);
                    await _context.SaveChangesAsync();
                    TempData["Exito"] = "Estudiante editado ok";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteUHExists(estudianteUH.Id))
                    {
                        TempData["Exito"] = "Estudiante no se encontro";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estudianteUH);
        }

        // GET: EstudianteUHs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudianteUH = await _context.EstudianteUH
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteUH == null)
            {
                return NotFound();
            }

            return View(estudianteUH);
        }

        // POST: EstudianteUHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudianteUH = await _context.EstudianteUH.FindAsync(id);
            if (estudianteUH != null)
            {
                _context.EstudianteUH.Remove(estudianteUH);
            }

            await _context.SaveChangesAsync();
            TempData["Exito"] = "Estudiante borrado ok";
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteUHExists(int id)
        {
            return _context.EstudianteUH.Any(e => e.Id == id);
        }
    }
}
