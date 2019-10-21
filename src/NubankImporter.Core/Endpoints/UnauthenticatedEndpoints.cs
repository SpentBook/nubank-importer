using System.Collections.Generic;
using NubankImporter.Core.HttpClient;

namespace NubankImporter.Core.Endpoints
{
    public class UnauthenticatedEndpoints : IUnauthenticatedEndpoints
    {
        private const string DiscoveryUrl = "https://prod-s0-webapp-proxy.nubank.com.br/api/discovery";
        private const string DiscoveryAppUrl = "https://prod-s0-webapp-proxy.nubank.com.br/api/app/discovery";
        private readonly IHttpClient httpClient;

        private string login;
        private string lift;

        public UnauthenticatedEndpoints(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public string Login => GetLoginUrl();

        public string Lift => GetLiftUrl();

        private string GetLoginUrl()
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                Discover();
            }

            return login;
        }

        private string GetLiftUrl()
        {
            if (string.IsNullOrWhiteSpace(lift))
            {
                DiscoverApp();
            }

            return lift;
        }

        private void Discover()
        {
            var response = httpClient.GetAsync<Dictionary<string, string>>(DiscoveryUrl)
                .GetAwaiter().GetResult();
            login = response["login"];
        }

        private void DiscoverApp()
        {
            var response = httpClient.GetAsync<Dictionary<string, string>>(DiscoveryAppUrl)
                .GetAwaiter().GetResult();
            lift = response["lift"];
        }
    }
}