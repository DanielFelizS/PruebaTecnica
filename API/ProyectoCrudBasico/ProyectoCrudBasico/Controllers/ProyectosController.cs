using Microsoft.AspNetCore.Mvc;
using ProyectoCrudBasico.Data;
using ProyectoCrudBasico.Models;

namespace ProyectoCrudBasico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController : ControllerBase
{
    private readonly DataContext _context;

    public ProyectosController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult ObtenerProyectos() => Ok(_context.Proyectos.ToList());

    [HttpGet("{id:int}", Name = "VerProyectoPorId")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult VerProyectoPorId(int id)
    {
        var proyecto = _context.Proyectos.Find(id);
        if (proyecto == null) return NotFound();
        return Ok(proyecto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Proyecto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult CrearProyecto([FromBody] Proyecto proyecto)
    {
        var existeNombre = _context.Proyectos.Any(p => p.Nombre.ToLower() == proyecto.Nombre.ToLower());
        if (existeNombre) return BadRequest("Ya existe un proyecto con ese nombre.");
        _context.Proyectos.Add(proyecto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObtenerProyectos), new { id = proyecto.Id }, proyecto);
    }

    [HttpPut("{id:int}", Name = "ActualizarProyecto")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult ActualizarProyecto(int id, [FromBody] Proyecto updated)
    {
        var proyecto = _context.Proyectos.Find(id);
        if (proyecto == null) return NotFound();

        proyecto.Nombre = updated.Nombre;
        proyecto.Descripcion = updated.Descripcion;

        _context.SaveChanges();
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public IActionResult EliminarProyecto(int id)
    {
        var proyecto = _context.Proyectos.Find(id);
        if (proyecto == null) return NotFound();

        _context.Proyectos.Remove(proyecto);
        _context.SaveChanges();
        return NoContent();
    }
}
