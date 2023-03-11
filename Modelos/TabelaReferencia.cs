using System.Text.Json.Serialization;

namespace Fipe.Modelos;

public class TabelaReferencia
{
    public int Codigo { get; init; }

    public string Mes { get; init; } = string.Empty;
}