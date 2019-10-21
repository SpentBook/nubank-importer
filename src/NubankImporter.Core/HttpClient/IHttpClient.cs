using System.Collections.Generic;
using System.Threading.Tasks;

namespace NubankImporter.Core.HttpClient
{
    public interface IHttpClient
    {
        Task<T> GetAsync<T>(string url) where T : new();
        Task<T> GetAsync<T>(string url, IDictionary<string, string> headers) where T : new();
        Task<T> PostAsync<T>(string url, object body) where T : new();
        Task<T> PostAsync<T>(string url, object body, IDictionary<string, string> headers) where T : new();
    }
}