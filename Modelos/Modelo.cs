namespace Fipe.Modelos;
using System.Text.Json.Serialization;

public class Modelo
{
    [JsonPropertyName("Label")]
    public string Nome { get; init; } = string.Empty;

    [JsonPropertyName("Value")]
    public int Codigo { get; init; }
}