using System.Net.Http.Json;

public class LocalidadeService
{
    private readonly HttpClient _http;

    public LocalidadeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Estado>> GetEstadosAsync()
    {
        return await _http.GetFromJsonAsync<List<Estado>>(
            "https://servicodados.ibge.gov.br/api/v1/localidades/estados"
        ) ?? new List<Estado>() ;
    }

    public async Task<List<Cidade>> GetCidadesByUFAsync(string uf)
    {
        return await _http.GetFromJsonAsync<List<Cidade>>(
            $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios"
        ) ?? new List<Cidade>();
    }
}

public class Estado
{
    public int id { get; set; }
    public string sigla { get; set; }
    public string nome { get; set; }
}

public class Cidade
{
    public int id { get; set; }
    public string nome { get; set; }
}