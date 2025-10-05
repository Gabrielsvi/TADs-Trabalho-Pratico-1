using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Model
{
    public class Veiculo
    {
        public int VeiculoId { get; set; }

        [Required, StringLength(100)]
        public string Modelo { get; set; } = null!;

        [Range(1900, 2100)]
        public int AnoFabricacao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Quilometragem { get; set; }

        [Required, StringLength(10)]
        public string Placa { get; set; } = null!;

        public bool Disponivel { get; set; } = true;

        public int FabricanteId { get; set; }
        public Fabricante Fabricante { get; set; } = null!;
        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}
