namespace Fipe.Entidades;

public class MarcaCarro
{
    public string Codigo { get; init; } = string.Empty;
    public string Nome { get; init; } = string.Empty;
    public List<ModeloCarro> Modelos { get; set; } = new();

}