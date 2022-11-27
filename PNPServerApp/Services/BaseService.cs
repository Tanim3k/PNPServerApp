using Microsoft.Net.Http.Headers;
using PNPServerApp.Models;
using System.Net.Http;
using System.Security.Claims;

namespace PNPServerApp.Services
{
    public abstract class BaseService
    {
        public static List<UserModel> registerUsers = new List<UserModel>();

        public IHttpContextAccessor HttpContextAccessor { get; }
        public IHttpClientFactory HttpClientFactory { get; }

        protected BaseService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            HttpContextAccessor = httpContextAccessor;
            HttpClientFactory = httpClientFactory;
        }

        public UserModel? GetUserByUserName(string userName)
        {
            return string.IsNullOrEmpty(userName) ? null : registerUsers.FirstOrDefault(m => m.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public UserModel? GetCurrentUser()
        {
            var identity = HttpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return registerUsers.FirstOrDefault(m => m.UserName.Equals(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value));
            }
            return null;
        }

        //protected JsonContent GetHttpClient(string url, Parameter)
        //{
        //    var httpRequestMessage = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
        //    {
        //        Headers =
        //    {
        //        { HeaderNames.Accept, "application/vnd.github.v3+json" },
        //        { HeaderNames.UserAgent, "HttpRequestsSample" }
        //        }
        //    };

        //    var httpClient = _httpClientFactory.CreateClient();
        //    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        //    if (httpResponseMessage.IsSuccessStatusCode)
        //    {
        //        using var contentStream =
        //            await httpResponseMessage.Content.ReadAsStreamAsync();

        //        GitHubBranches = await JsonSerializer.DeserializeAsync
        //            <IEnumerable<GitHubBranch>>(contentStream);
        //    }
        //}
    }
}
