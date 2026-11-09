using HeroesFluent.Data;
using HeroesFluent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HeroesFluent.Controllers;

[Authorize(Roles = "Administrador")]
public class EquiposController : Controller
{
    private readonly HeroesDbContext _context;
    public EquiposController(HeroesDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.Equipos.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var equipo = await _context.Equipos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        return equipo == null ? NotFound() : View(equipo);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,FechaFundacion,Presupuesto")] Equipo equipo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(equipo);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var equipo = await _context.Equipos.FindAsync(id.Value);
        return equipo == null ? NotFound() : View(equipo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaFundacion,Presupuesto")] Equipo equipo)
    {
        if (id != equipo.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(equipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Equipos.AnyAsync(e => e.Id == equipo.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(equipo);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var equipo = await _context.Equipos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        return equipo == null ? NotFound() : View(equipo);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo != null)
        {
            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
