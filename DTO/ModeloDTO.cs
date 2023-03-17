namespace Fipe.DTO;
using System.Text.Json.Serialization;
using Fipe.Entidades;

public class ModeloDTO
{
    [JsonPropertyName("Value")]
    public int Codigo { get; init; }

    [JsonPropertyName("Label")]
    public string Nome { get; init; } = string.Empty;

    public ModeloCarro DTOParaEntidade() => new()
    {
        Codigo = Codigo,
        Nome = Nome
    };

}