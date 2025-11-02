using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Exceptions;
using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Services;
using System.Net.Http.Json;
using System.Web;

namespace PocketbaseNetSdk
{
    public class PocketbaseClient
    {
        private HttpClient HttpClient { get; init; }
        private readonly ILogger<PocketbaseClient>? _logger;
        readonly ITokenService _tokenService;

        public ITokenService TokenService => _tokenService;

        public PocketbaseClient(string baseUrl, ITokenService? tokenService = null, ILogger<PocketbaseClient>? logger = null, HttpClient? httpClient = null)
        {
            _logger = logger;
            HttpClient = httpClient ?? new HttpClient();

            HttpClient.BaseAddress = new Uri(baseUrl);

            _tokenService = tokenService ?? new TokenService();
        }

        public async Task<Result<T?>> SendJsonRequestAsync<T, R>(HttpMethod httpMethod, string path, R? requestBody = null, 
            IDictionary<string, object?>? queryParameters = null) where R : class
        {
            _logger?.LogTrace("Sending {Method} request to {Path}", httpMethod.Method, path);

            var uri = BuildUri(path, queryParameters);

            var requestMessage = CreateRequest(httpMethod, uri, requestBody);

            if (!requestMessage.Headers.Contains("Authorization") && _tokenService.IsValid())
            {
                requestMessage.Headers.Add("Authorization", _tokenService.Token);
            }

            var response = await HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogDebug("Request to {Path} failed with status code {StatusCode}", path, response.StatusCode);

                var error = await response.Content.ReadFromJsonAsync<PocketbaseServerError>()
                    .ConfigureAwait(false);

                return Result<T?>.Failure($"Request to {path} failed with status code {response.StatusCode}: {error?.Message}");
            }
            _logger?.LogDebug("Request to {Path} succeeded with status code {StatusCode}", path, response.StatusCode);

            T? result;
            try
            {
                result = await response.Content.ReadFromJsonAsync<T>()
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                result = default;
            }


            return Result<T?>.Success(result);
        }

        private Uri BuildUri(string path, IDictionary<string, object?>? queryParameters = null)
        {
            _logger?.LogTrace("Building URI for path: {Path}", path);

            var url = new UriBuilder(new Uri(HttpClient.BaseAddress!, path));

            //Add query parameters
            if (queryParameters is not null && queryParameters.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(url.Query);
                foreach (var param in from param in queryParameters
                                      where param.Value is not null
                                      select param)
                {
                    query[param.Key] = param.Value.ToString();
                }

                url.Query = query.ToString() ?? string.Empty;
            }

            _logger?.LogDebug("Built URI: {Uri}", url.Uri);
            return url.Uri;
        }

        private static HttpRequestMessage CreateRequest<R> (HttpMethod method, Uri uri, R? body = null) where R : class
        {
            var request = new HttpRequestMessage(method, uri);
            if (body is not null)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }
            return request;
        }
    }
}