using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NubankImporter.Core.Endpoints;
using NubankImporter.Core.HttpClient;
using NubankImporter.Core.Responses;

namespace NubankImporter.Core
{
    public class NubankClient : INubankClient
    {
        private readonly IHttpClient httpClient;
        private readonly IUnauthenticatedEndpoints unauthenticatedEndpoints;

        public NubankClient(
            IHttpClient httpClient,
            IUnauthenticatedEndpoints unauthenticatedEndpoints)
        {
            this.httpClient = httpClient;
            this.unauthenticatedEndpoints = unauthenticatedEndpoints;
        }

        public async Task<LoginResponse> Login(string cpf, string password)
        {
            var body = new
            {
                client_id = "other.conta",
                client_secret = "yQPeLzoHuJzlMMSAjC-LgNUJdUecx8XO",
                grant_type = "password",
                login = cpf,
                password = password
            };
            var response = await httpClient.PostAsync<Dictionary<string, object>>(unauthenticatedEndpoints.Login, body);

            return new LoginResponse
            {
                NeedsDeviceAuthorization = true,
                Token = GetTokenFromResponse(response)
            };
        }

        public async Task<AuthenticatedResponse> AuthenticateWithQrCode(string code, string accessToken)
        {
            var payload = new
            {
                qr_code_id = code,
                type = "login-webapp"
            };

            var response = await httpClient.PostAsync<Dictionary<string, object>>(unauthenticatedEndpoints.Lift, payload, GetAuthorizationHeader(accessToken));

            return new AuthenticatedResponse
            {
                AccessToken = GetTokenFromResponse(response),
                AuthenticatedEndpoints = GetEndpointsFromResponse(response)
            };
        }

        public async Task<AuthenticatedResponse> AuthenticateWithAccessToken(string accessToken)
        {
            return new AuthenticatedResponse();
        }

        private Dictionary<string, string> GetAuthorizationHeader(string accessToken)
        {
            return new Dictionary<string, string> {
                { "Authorization", $"Bearer {accessToken}" }
            };
        }

        private string GetTokenFromResponse(Dictionary<string, object> response)
        {
            return response["access_token"].ToString();
        }

        private AuthenticatedEndpoints GetEndpointsFromResponse(Dictionary<string, object> response)
        {
            var listLinks = ((Dictionary<string, object>)response["_links"]);
            var listLinksConverted = listLinks
                .Select(x => new KeyValuePair<string, string>(x.Key, (((Dictionary<string, object>)x.Value)["href"].ToString())))
                .ToDictionary(x => x.Key, x => x.Value);

            return new AuthenticatedEndpoints
            {
                BillsSummary = listLinksConverted["bills_summary"]
            };
        }
    }
}