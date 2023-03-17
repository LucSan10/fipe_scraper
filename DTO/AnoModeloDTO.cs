namespace Fipe.DTO;

public class AnoModeloDTO
{
    public List<ModeloDTO> Modelos { get; init; } = new();
    public List<AnoDTO> Anos { get; init; } = new();
}