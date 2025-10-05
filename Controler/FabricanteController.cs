using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Model;

namespace LocadoraVeiculos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FabricanteController : ControllerBase
    {
        private readonly LocadoraContext _context;
        public FabricanteController(LocadoraContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Fabricantes.Include(f => f.Veiculos).ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var f = await _context.Fabricantes.Include(x => x.Veiculos).FirstOrDefaultAsync(x => x.FabricanteId == id);
            if (f == null) return NotFound();
            return Ok(f);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fabricante f)
        {
            _context.Fabricantes.Add(f);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = f.FabricanteId }, f);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Fabricante f)
        {
            if (id != f.FabricanteId) return BadRequest();
            _context.Entry(f).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var f = await _context.Fabricantes.FindAsync(id);
            if (f == null) return NotFound();
            _context.Fabricantes.Remove(f);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
