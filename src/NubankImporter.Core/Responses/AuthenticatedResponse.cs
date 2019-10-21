using NubankImporter.Core.Endpoints;

namespace NubankImporter.Core.Responses
{
    public class AuthenticatedResponse
    {
        public string AccessToken { get; set; }

        public AuthenticatedEndpoints AuthenticatedEndpoints { get; set; }
    }
}