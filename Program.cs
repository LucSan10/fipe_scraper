using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Fipe.Entidades;
using Fipe.DTO;

var options = new JsonSerializerOptions
{
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    WriteIndented = true
};

HttpClient client = new();

const string tabelaReferenciaURL = "https://brasilapi.com.br/api/fipe/tabelas/v1";
const string fipeURL = "https://veiculos.fipe.org.br/api/veiculos";

var tabelasReferencia = await client.GetFromJsonAsync<List<TabelaReferencia>>(tabelaReferenciaURL);
var primeiraTabelaRef = tabelasReferencia![0].Codigo.ToString();

var payload = new Dictionary<string, string>
{
    { "codigoTabelaReferencia", primeiraTabelaRef },
    { "codigoTipoVeiculo", "1" }
};

var body = new FormUrlEncodedContent(payload);

var response = await client.PostAsync($"{fipeURL}/ConsultarMarcas", body);
var marcas = await response.Content.ReadFromJsonAsync<List<MarcaDTO>>();
var marcasCarro = new List<MarcaCarro>();

foreach (MarcaDTO marca in marcas!)
{
    Console.WriteLine(marca.Nome);
    var marcaCarro = marca.DTOParaEntidade();

    payload["codigoMarca"] = marca.Codigo;

    body = new FormUrlEncodedContent(payload);
    response = await client.PostAsync($"{fipeURL}/ConsultarModelos", body);

    var anoModelos = await response.Content.ReadFromJsonAsync<AnoModeloDTO>();

    foreach (ModeloDTO modelo in anoModelos!.Modelos)
    {
        payload["codigoModelo"] = modelo.Codigo.ToString();
        body = new FormUrlEncodedContent(payload);

        response = await client.PostAsync($"{fipeURL}/ConsultarAnoModelo", body);
        var anos = await response.Content.ReadFromJsonAsync<List<AnoDTO>>();

        var modeloCarro = modelo.DTOParaEntidade();
        modeloCarro.Anos.AddRange(anos!
            .ConvertAll(a => a.DTOParaEntidade())
            .GroupBy(a => a.Ano)
            .Select(a => a.First())
            .ToList());
        marcaCarro.Modelos.Add(modeloCarro);
    }

    marcasCarro.Add(marcaCarro);
}

File.WriteAllText("marcas.json", JsonSerializer.Serialize(marcasCarro, options));