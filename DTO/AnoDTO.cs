namespace Fipe.DTO;
using System.Text.Json.Serialization;
using Fipe.Entidades;

public class AnoDTO
{
    [JsonPropertyName("Label")]
    public string NomeAnoTipo { get; init; } = string.Empty;

    [JsonPropertyName("Value")]
    public string CodigoAnoTipo { get; init; } = string.Empty;

    public AnoModeloCarro DTOParaEntidade()
    {
        var codigo = CodigoAnoTipo.Split("-")[0];

        return new AnoModeloCarro
        {
            Codigo = codigo,
            Ano = codigo == "32000" ? "Zero KM" : codigo
        };
    }
}