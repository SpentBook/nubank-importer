using System.Threading.Tasks;
using NubankImporter.Core.Responses;

namespace NubankImporter.Core
{
    public interface INubankClient
    {
         Task<LoginResponse> Login(string cpf, string password);

         Task<AuthenticatedResponse> AuthenticateWithAccessToken(string accessToken);

         Task<AuthenticatedResponse> AuthenticateWithQrCode(string code, string accessToken);
    }
}