using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Model;

namespace LocadoraVeiculos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AluguelController : ControllerBase
    {
        private readonly LocadoraContext _context;
        public AluguelController(LocadoraContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alugueis = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo).ThenInclude(v => v.Fabricante)
                .Include(a => a.Pagamentos)
                .AsNoTracking()
                .ToListAsync();
            return Ok(alugueis);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluguel = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .FirstOrDefaultAsync(a => a.AluguelId == id);

            if (aluguel == null) return NotFound();
            return Ok(aluguel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Aluguel aluguel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = aluguel.AluguelId }, aluguel);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Aluguel aluguel)
        {
            if (id != aluguel.AluguelId) return BadRequest();
            _context.Entry(aluguel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null) return NotFound();
            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🔎 Filtro: alugueis ativos (sem devolução)
        [HttpGet("ativos")]
        public async Task<IActionResult> GetAtivos()
        {
            var result = await _context.Alugueis
                .Where(a => a.DataDevolucao == null)
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();
            return Ok(result);
        }
    }
}
