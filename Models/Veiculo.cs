public class Veiculo
{
    public int VeiculoId { get; set; }
    public string Modelo { get; set; } = null!;
    public int AnoFabricacao { get; set; }
    public decimal Quilometragem { get; set; }
    public string Placa { get; set; } = null!;
    public bool Disponivel { get; set; } = true;

    public int FabricanteId { get; set; }
    public Fabricante Fabricante { get; set; } = null!;
    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
