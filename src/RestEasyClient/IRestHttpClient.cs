using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestEasyClient
{
    public interface IRestHttpClient
    {
        Uri BaseAddress { get; set; }

        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);

        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);

        Task<HttpResponseMessage> GetAsync(string requestUri);

        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}