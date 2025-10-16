using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prog5_3C2025.Data;
using Prog5_3C2025.Models;

namespace Prog5_3C2025.Controllers
{
    public class EnfermedadesController : Controller
    {
        private readonly AppDbContext _context;

        public EnfermedadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Enfermedades
        public async Task<IActionResult> Index()
        {
            return _context.Enfermedades != null ?
                        View(await _context.Enfermedades.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Enfermedades'  is null.");
        }

        [HttpPost]
        public IActionResult Index(string enfermedad)
        {
            List<Enfermedades> enfermedades = BuscarEnfermedades(enfermedad);
            return View(enfermedades);
        }

        [NonAction]
        private static List<Enfermedades> BuscarEnfermedades(string enfermedad)
        {
            List<Enfermedades> enfermedades = new List<Enfermedades>();
            string apiUrl = "https://localhost:7285/api/ObtenerEnfermedades";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/ObtenerEnfermedades?enfermedad={0}", enfermedad)).Result;
            if (response.IsSuccessStatusCode)
            {
                enfermedades = JsonConvert.DeserializeObject<List<Enfermedades>>(response.Content.ReadAsStringAsync().Result);
            }

            return enfermedades;
        }

        // GET: Enfermedades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedades = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enfermedades == null)
            {
                return NotFound();
            }

            return View(enfermedades);
        }

        // GET: Enfermedades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermedades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Enfermedad,Creada")] Enfermedades enfermedades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermedades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermedades);
        }

        // GET: Enfermedades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedades = await _context.Enfermedades.FindAsync(id);
            if (enfermedades == null)
            {
                return NotFound();
            }
            return View(enfermedades);
        }

        // POST: Enfermedades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Enfermedad,Creada")] Enfermedades enfermedades)
        {
            if (id != enfermedades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermedades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadesExists(enfermedades.Id))
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
            return View(enfermedades);
        }

        // GET: Enfermedades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermedades = await _context.Enfermedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enfermedades == null)
            {
                return NotFound();
            }

            return View(enfermedades);
        }

        // POST: Enfermedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermedades = await _context.Enfermedades.FindAsync(id);
            if (enfermedades != null)
            {
                _context.Enfermedades.Remove(enfermedades);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadesExists(int id)
        {
            return _context.Enfermedades.Any(e => e.Id == id);
        }
    }
}
