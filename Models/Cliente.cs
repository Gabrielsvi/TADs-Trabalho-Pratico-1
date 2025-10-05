using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = null!;

        [Required, StringLength(11)]
        public string CPF { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? Telefone { get; set; }

        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}
