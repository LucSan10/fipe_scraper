namespace Fipe.Modelos;
using System.Text.Json.Serialization;

public class AnoModelo
{
    public List<Modelo> Modelos { get; init; } = new();
    public List<Ano> Anos { get; init; } = new();
}