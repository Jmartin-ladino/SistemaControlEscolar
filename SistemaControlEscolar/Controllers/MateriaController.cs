using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaControlEscolar.Models;

namespace SistemaControlEscolar.Controllers
{
    public class MateriaController : Controller
    {
        private readonly ControlEscolarContext _context;

        public MateriaController(ControlEscolarContext context)
        {
            _context = context;
        }

        // GET: Materia
        public async Task<IActionResult> Index()
        {
            var controlEscolarContext = _context.Materia.Include(m => m.IdAlumnoNavigation);
            return View(await controlEscolarContext.ToListAsync());
        }

        // GET: Materia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Materia == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia
                .Include(m => m.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materium == null)
            {
                return NotFound();
            }

            return View(materium);
        }

        // GET: Materia/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Alumno1");
            return View();
        }

        // POST: Materia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Materia,Calificacion,Acreditada,IdAlumno")] Materium materium)
        {

            materium.Materia = Request.Form["Materia"];
            materium.Calificacion = int.Parse(Request.Form["Calificacion"]!);
            materium.Acreditada = bool.Parse(Request.Form["Acreditada"]!);
            materium.IdAlumno = int.Parse(Request.Form["IdAlumno"]!);
            _context.Add(materium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(materium);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Id", materium.IdAlumno);
            return View(materium);
        }

        // GET: Materia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materia == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia.FindAsync(id);
            if (materium == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Alumno1", materium.IdAlumno);
            return View(materium);
        }

        // POST: Materia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Materia,Calificacion,Acreditada,IdAlumno")] Materium materium)
        {
            if (id != materium.Id)
            {
                return NotFound();
            }

            materium.Materia = Request.Form["Materia"];
            materium.Calificacion = int.Parse(Request.Form["Calificacion"]!);
            materium.Acreditada = bool.Parse(Request.Form["Acreditada"]!);
            materium.IdAlumno = int.Parse(Request.Form["IdAlumno"]!);
            try
                {
                    _context.Update(materium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriumExists(materium.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "Id", "Id", materium.IdAlumno);
            return View(materium);
        }

        // GET: Materia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Materia == null)
            {
                return NotFound();
            }

            var materium = await _context.Materia
                .Include(m => m.IdAlumnoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materium == null)
            {
                return NotFound();
            }

            return View(materium);
        }

        // POST: Materia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Materia == null)
            {
                return Problem("Entity set 'ControlEscolarContext.Materia'  is null.");
            }
            var materium = await _context.Materia.FindAsync(id);
            if (materium != null)
            {
                _context.Materia.Remove(materium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriumExists(int id)
        {
          return (_context.Materia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
