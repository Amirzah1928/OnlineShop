using Microsoft.Extensions.Options;
using OnlineShop.DomainService.Proxies;
using System.Text.Json;

namespace OnlineShop.Infrastructure.Proxies;

public class TrackingCodeProxy : ITrackingCodeProxy
{
    private readonly Settings _settings;
    private readonly HttpClient _httpClient;

    public TrackingCodeProxy(IOptions<Settings> options, HttpClient httpClient)
    {
        _settings = options.Value;

        httpClient.BaseAddress = new Uri(_settings.TrackingCode.BaseURL);
        _httpClient = httpClient;
    }

    public int Priority => _settings.PriorityConfig.TrackingCodeProxy;

    public async Task<List<string>> Get(int count, CancellationToken cancellationToken)
    {
        var url = string.Format(_settings.TrackingCode.GetURL, _settings.TrackingCode.Prefix, count);

        using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("TrackingCode Not Available");
        }

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

        var objectResult = JsonSerializer.Deserialize<GetTrackingCodeViewModel>(stringResult,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        return objectResult.Codes;
    }
}

