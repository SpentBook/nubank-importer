namespace NubankImporter.Core.Endpoints
{
    public interface IUnauthenticatedEndpoints
    {
        string Login { get; }

        string Lift { get; }
    }
}