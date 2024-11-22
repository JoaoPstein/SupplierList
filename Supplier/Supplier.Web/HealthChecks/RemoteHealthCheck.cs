using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Supplier.Web;

public class RemoteHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    public RemoteHealthCheck(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var client = _httpClientFactory.CreateClient();

        // Obter token de autenticação
        var token = await GetAuthTokenAsync();

        if (string.IsNullOrEmpty(token))
        {
            return HealthCheckResult.Unhealthy("Falha ao obter token de autenticação.");
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync(_configuration["ExternalApi:HealthEndpoint"], cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return HealthCheckResult.Healthy("API externa está respondendo corretamente.");
        }
        else
        {
            return HealthCheckResult.Unhealthy($"API externa retornou status code {response.StatusCode}.");
        }
    }
    
    private async Task<string> GetAuthTokenAsync()
    {
        // Implementar lógica para obter o token de autenticação usando usuário e senha
        // Exemplo:
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsync(_configuration["ExternalApi:TokenEndpoint"], new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", _configuration["ExternalApi:Username"]),
            new KeyValuePair<string, string>("password", _configuration["ExternalApi:Password"]),
            new KeyValuePair<string, string>("grant_type", "password")
        }));

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<JsonElement>(content);
            return tokenResponse.GetProperty("access_token").GetString();
        }

        return null;
    }
}
