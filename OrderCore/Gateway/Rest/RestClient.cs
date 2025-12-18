using System.Text;
using System.Text.Json;

namespace Gateway.Rest;

public class RestClient<TSend, TReceive>
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public RestClient(string baseUrl)
    {
        _baseUrl = baseUrl;
        _client = new HttpClient();

    }

    public async Task<List<TSend>> GetListRequest(string url)
    {
        var response = await _client.GetAsync(_baseUrl + url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<TSend>>(json)!;
    }
    public async Task<TSend> GetRequest(string url)
    {
        var response = await _client.GetAsync(_baseUrl + url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TSend>(json)!;
    }
    public async Task<TSend> PostRequest(string url, TReceive body)
    {
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(_baseUrl + url, content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TSend>(result)!;
    }
    public async Task DeleteRequest(string url)
    {
        var response = await _client.DeleteAsync(_baseUrl + url);
        response.EnsureSuccessStatusCode();
    }

}
