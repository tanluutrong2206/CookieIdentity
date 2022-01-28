using CookieIdentity.Models;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;

using System.Net.Http.Headers;
using System.Text;

namespace CookieIdentity.AppCode
{
    public class PageEndPointModel : PageModel
    {
        private readonly IHttpClientFactory clientFactory;
        public PageEndPointModel(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<T> InvokeGetEndPointAsync<T>(string clientName, string url) where T : new()
        {
            JwtToken token = await InitToken();
            var httpClient = clientFactory.CreateClient(clientName);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return await httpClient.GetFromJsonAsync<T>(url)
                ?? new();
        }
        public async Task<T> InvokePostEndPointAsync<T, U>(string clientName, string url, U body) where T : new()
        {
            JwtToken token = await InitToken();
            var httpClient = clientFactory.CreateClient(clientName);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var res = await httpClient.PostAsJsonAsync(url, body)
                ?? new();
            res.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync()) ?? new();
        }
        public async Task<T> InvokePutEndPointAsync<T, U>(string clientName, string url, U body) where T : new()
        {
            JwtToken token = await InitToken();
            var httpClient = clientFactory.CreateClient(clientName);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var res = await httpClient.PutAsJsonAsync(url, body)
                ?? new();
            res.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync()) ?? new();
        }
        public async Task<T> InvokeDeleteEndPointAsync<T>(string clientName, string url) where T : new()
        {
            JwtToken token = await InitToken();
            var httpClient = clientFactory.CreateClient(clientName);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var res = await httpClient.DeleteAsync(url)
                ?? new();
            res.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync()) ?? new();
        }

        private async Task<JwtToken> InitToken()
        {
            JwtToken token;
            var jwtToken = HttpContext.Session.GetString(Constant.JWT_TOKEN);
            if (string.IsNullOrEmpty(jwtToken))
            {
                token = await AuthenticateJwt();
            }
            else
            {
                token = JsonConvert.DeserializeObject<JwtToken>(jwtToken) ?? new JwtToken();
            }
            if (string.IsNullOrEmpty(token.AccessToken) || token.ExpireDate <= DateTime.UtcNow)
            {
                token = await AuthenticateJwt();
            }
            return token;
        }

        private async Task<JwtToken> AuthenticateJwt()
        {
            var httpClient = clientFactory.CreateClient("WebAPI");
            var res = await httpClient.PostAsJsonAsync("Auth", new Credential { Password = "password", UserName = "admin" });
            res.EnsureSuccessStatusCode();
            var strJwt = await res.Content.ReadAsStringAsync();
            HttpContext.Session.SetString(Constant.JWT_TOKEN, strJwt);
            return JsonConvert.DeserializeObject<JwtToken>(strJwt) ?? new JwtToken();
        }
    }
}
