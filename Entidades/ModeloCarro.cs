namespace Fipe.Entidades;

public class ModeloCarro
{
    public int Codigo { get; init; }
    public string Nome { get; init; } = string.Empty;
    public List<AnoModeloCarro> Anos { get; init; } = new();
}