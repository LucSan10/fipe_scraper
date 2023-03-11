using System.Text.Json.Serialization;

namespace Fipe.Modelos;

public class Marca
{
    [JsonPropertyName("Value")]
    public string Codigo { get; init; } = string.Empty;

    [JsonPropertyName("Label")]
    public string Nome { get; init; } = string.Empty;
}