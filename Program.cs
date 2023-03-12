using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Fipe.Modelos;

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
var marcas = await response.Content.ReadFromJsonAsync<List<Marca>>();
var modelosMarcas = new List<ModelosMarca>();

foreach (Marca marca in marcas!)
{
    payload = new Dictionary<string, string>
    {
        { "codigoTabelaReferencia", primeiraTabelaRef },
        { "codigoTipoVeiculo", "1" },
        { "codigoMarca", marca.Codigo }
    };

    body = new FormUrlEncodedContent(payload);

    response = await client.PostAsync($"{fipeURL}/ConsultarModelos", body);
    var anoModelos = await response.Content.ReadFromJsonAsync<AnoModelo>();

    var modelosMarca = new ModelosMarca { Marca = marca, Modelos = anoModelos!.Modelos };
    modelosMarcas.Add(modelosMarca);
}

var options = new JsonSerializerOptions
{
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    WriteIndented = true
};

modelosMarcas = modelosMarcas.OrderByDescending(mm => mm.Modelos.Count).ToList();
modelosMarcas.ForEach(mm => Console.WriteLine($"{mm.Marca.Nome}: {mm.Modelos.Count}\n"));

var total = modelosMarcas.Aggregate(0, (acc, mm) => acc + mm.Modelos.Count);
Console.WriteLine($"total: {total}");