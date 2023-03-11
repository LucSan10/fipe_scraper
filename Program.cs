using System.Net.Http.Json;
using System.Text.Json;
using Fipe.Modelos;

HttpClient client = new();

const string tabelaReferenciaURL = "https://brasilapi.com.br/api/fipe/tabelas/v1";
const string fipeURL = "https://veiculos.fipe.org.br/api/veiculos";

var tabelasReferencia = await client.GetFromJsonAsync<List<TabelaReferencia>>(tabelaReferenciaURL);

var payload = new Dictionary<string, string>
{
    { "codigoTabelaReferencia", tabelasReferencia![0].Codigo.ToString() },
    { "codigoTipoVeiculo", "1" }
};

var body = new FormUrlEncodedContent(payload);

var response = await client.PostAsync(fipeURL + "/ConsultarMarcas", body);
var marcas = await response.Content.ReadFromJsonAsync<List<Marca>>();
var modelosMarcas = new List<ModelosMarca>();

foreach (Marca marca in marcas!)
{
    payload = new Dictionary<string, string>
    {
        { "codigoTabelaReferencia", tabelasReferencia![0].Codigo.ToString() },
        { "codigoTipoVeiculo", "1" },
        { "codigoMarca", marca.Codigo }
    };

    body = new FormUrlEncodedContent(payload);

    response = await client.PostAsync(fipeURL + "/ConsultarModelos", body);
    var anoModelos = await response.Content.ReadFromJsonAsync<AnoModelo>();

    var modelosMarca = new ModelosMarca { Marca = marca, Modelos = anoModelos!.Modelos };
    modelosMarcas.Add(modelosMarca);
}

var count = 0;
foreach (ModelosMarca mm in modelosMarcas)
{
    count += mm.Modelos.Count;
    Console.WriteLine($"{mm.Marca.Nome}: {mm.Modelos.Count}");

    Console.WriteLine($"Total: {count}\n");
}