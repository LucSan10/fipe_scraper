namespace Fipe.DTO;

public class ModelosMarcaDTO
{
    public MarcaDTO Marca { get; set; } = new();
    public List<ModeloDTO> Modelos { get; set; } = new();
}