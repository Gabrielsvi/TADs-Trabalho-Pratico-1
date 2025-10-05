using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Model
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }

        public int AluguelId { get; set; }
        public Aluguel Aluguel { get; set; } = null!;

        [Required]
        public DateTime DataPagamento { get; set; }

        [Required, StringLength(50)]
        public string FormaPagamento { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal ValorPago { get; set; }
    }
}
