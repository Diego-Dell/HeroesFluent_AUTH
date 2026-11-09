using HeroesFluent.Data;
using HeroesFluent.Models;
using HeroesFluent.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HeroesFluent.Controllers;

[Authorize]
public class HeroesController : Controller
{
    [HttpGet]
    [Authorize(Roles = "Administrador,Lector")]
    public IActionResult ResumenSeguridad()
    {
        return Content("Acceso al resumen permitido para Administrador o Lector.");
    }

    private readonly HeroesDbContext _context;
    public HeroesController(HeroesDbContext context) => _context = context;

    private void CargarEquipos(int? equipoSeleccionado = null)
    {
        ViewData["EquipoId"] = new SelectList(
            _context.Equipos.AsNoTracking().OrderBy(e => e.Nombre),
            "Id", "Nombre", equipoSeleccionado);
    }

    public async Task<IActionResult> Index()
    {
        var heroes = await _context.Heroes.Include(h => h.Equipo).AsNoTracking().ToListAsync();
        return View(heroes);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var heroe = await _context.Heroes.Include(h => h.Equipo)
            .AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        return heroe == null ? NotFound() : View(heroe);
    }

    [Authorize(Policy = Permisos.CrearHeroes)]
    public IActionResult Create()
    {
        CargarEquipos();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permisos.CrearHeroes)]
    public async Task<IActionResult> Create([Bind("Nombre,Alias,NivelPoder,FechaRegistro,EquipoId")] Heroe heroe)
    {
        if (heroe.EquipoId.HasValue &&
            !await _context.Equipos.AnyAsync(e => e.Id == heroe.EquipoId.Value))
            ModelState.AddModelError(nameof(Heroe.EquipoId), "El equipo seleccionado no existe.");
        if (ModelState.IsValid)
        {
            _context.Add(heroe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    [Authorize(Policy = Permisos.EditarHeroes)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var heroe = await _context.Heroes.FindAsync(id.Value);
        if (heroe == null) return NotFound();
        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permisos.EditarHeroes)]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Nombre,Alias,NivelPoder,FechaRegistro,EquipoId")] Heroe heroe)
    {
        if (id != heroe.Id) return NotFound();
        if (heroe.EquipoId.HasValue &&
            !await _context.Equipos.AnyAsync(e => e.Id == heroe.EquipoId.Value))
            ModelState.AddModelError(nameof(Heroe.EquipoId), "El equipo seleccionado no existe.");
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(heroe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Heroes.AnyAsync(h => h.Id == heroe.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        CargarEquipos(heroe.EquipoId);
        return View(heroe);
    }

    [Authorize(Policy = Permisos.EliminarHeroes)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var heroe = await _context.Heroes.Include(h => h.Equipo)
            .AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        return heroe == null ? NotFound() : View(heroe);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permisos.EliminarHeroes)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var heroe = await _context.Heroes.FindAsync(id);
        if (heroe != null)
        {
            _context.Heroes.Remove(heroe);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
