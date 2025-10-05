using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Model
{
    public class Fabricante
    {
        public int FabricanteId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = null!;

        public string? PaisOrigem { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
