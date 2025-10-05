using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraVeiculos.Data;
using LocadoraVeiculos.Model;

namespace LocadoraVeiculos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly LocadoraContext _context;
        public PagamentoController(LocadoraContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Pagamentos.Include(p => p.Aluguel).ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _context.Pagamentos.FindAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pagamento.PagamentoId }, pagamento);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId) return BadRequest();
            _context.Entry(pagamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Pagamentos.FindAsync(id);
            if (p == null) return NotFound();
            _context.Pagamentos.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 🔎 Filtro: pagamentos por cliente
        [HttpGet("by-cliente/{clienteId:int}")]
        public async Task<IActionResult> GetByCliente(int clienteId)
        {
            var result = await _context.Pagamentos
                .Include(p => p.Aluguel)
                .ThenInclude(a => a.Cliente)
                .Where(p => p.Aluguel.ClienteId == clienteId)
                .ToListAsync();
            return Ok(result);
        }
    }
}
