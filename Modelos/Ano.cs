namespace Fipe.Modelos;
using System.Text.Json.Serialization;

public class Ano
{
    [JsonPropertyName("Label")]
    public string Nome { get; init; } = string.Empty;

    [JsonPropertyName("Value")]
    public string Codigo { get; init; } = string.Empty;
}