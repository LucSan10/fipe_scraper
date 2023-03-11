namespace Fipe.Modelos;
using System.Text.Json.Serialization;

public class ModelosMarca
{
    public Marca Marca { get; set; } = null!;
    public List<Modelo> Modelos { get; set; } = new();
}