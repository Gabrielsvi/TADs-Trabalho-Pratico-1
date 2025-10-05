using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Model
{
    public class Aluguel
    {
        public int AluguelId { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; } = null!;

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Range(0, double.MaxValue)]
        public decimal KmInicial { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? KmFinal { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ValorDiaria { get; set; }

        public decimal? ValorTotal { get; set; }

        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    }
}
