using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Model;

namespace LocadoraVeiculos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly LocadoraContext _context;
        public VeiculoController(LocadoraContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Veiculos.Include(v => v.Fabricante).AsNoTracking().ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _context.Veiculos.Include(x => x.Fabricante).FirstOrDefaultAsync(x => x.VeiculoId == id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = veiculo.VeiculoId }, veiculo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId) return BadRequest();
            _context.Entry(veiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var v = await _context.Veiculos.FindAsync(id);
            if (v == null) return NotFound();
            _context.Veiculos.Remove(v);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🔎 Filtro: veículos disponíveis
        [HttpGet("disponiveis")]
        public async Task<IActionResult> GetDisponiveis()
        {
            return Ok(await _context.Veiculos.Where(v => v.Disponivel).Include(v => v.Fabricante).ToListAsync());
        }
    }
}
