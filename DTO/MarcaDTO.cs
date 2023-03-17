namespace Fipe.DTO;
using System.Text.Json.Serialization;
using Fipe.Entidades;

public class MarcaDTO
{
    [JsonPropertyName("Value")]
    public string Codigo { get; init; } = string.Empty;

    [JsonPropertyName("Label")]
    public string Nome { get; init; } = string.Empty;

    public MarcaCarro DTOParaEntidade() => new()
    {
        Codigo = Codigo,
        Nome = Nome
    };

}