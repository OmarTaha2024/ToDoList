using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ToDoList.Data.Helpers;
using ToDoList.Data.Results;
using ToDoList.Service.Abstracts;
namespace ToDoList.Service.Services
{
    public class OAuthService : IOAuthService
    {
        private readonly OAuthSettings _settings;
        private readonly HttpClient _httpClient;
        public OAuthService(IOptions<OAuthSettings> settings, HttpClient httpClientFactory)
        {
            _settings = settings.Value;
            _httpClient = httpClientFactory;
        }

        public async Task<OAuthCallBack> GetAuthorizationCallBack(string code)
        {
            var parameters = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", _settings.ClientId },
            { "client_secret", _settings.ClientSecret },
            { "redirect_uri", _settings.RedirectUri },
            { "grant_type", "authorization_code" }
        };

            var content = new FormUrlEncodedContent(parameters);

            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<OAuthCallBack>(responseString);

            return tokenResponse;
        }

        public async Task<string> GetAuthorizationUrl()
        {
            var url =
                $"https://accounts.google.com/o/oauth2/v2/auth?" +
                $"client_id={_settings.ClientId}" +
                $"&redirect_uri={Uri.EscapeDataString(_settings.RedirectUri)}" +
                $"&response_type=code" +
                $"&scope={Uri.EscapeDataString("openid email profile")}" +
                $"&access_type=offline" +
                $"&prompt=consent";

            return url;
        }
    }
}
