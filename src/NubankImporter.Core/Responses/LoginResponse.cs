namespace NubankImporter.Core.Responses
{
    public class LoginResponse
    {
        public bool NeedsDeviceAuthorization { get; set; }
        public string Token { get; set; }
    }
}