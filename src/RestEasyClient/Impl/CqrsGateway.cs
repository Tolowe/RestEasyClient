using Newtonsoft.Json;
using RestEasyClient.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestEasyClient.Impl
{
    public class CqrsGateway<T> : ICqrsGateway<T>
    {
        private readonly IRestHttpClient _restHttpClient;

        public CqrsGateway(IRestHttpClient RestHttpClient)
        {
            _restHttpClient = RestHttpClient;
        }

        public void Create<C>(C CreateEntity)
        {
            Create(GetPathFromType(), CreateEntity);
        }

        public void Create<C>(string ResourcePath, C CreateEntity)
        {
            HttpContent body = new StringContent(JsonConvert.SerializeObject(CreateEntity));
            body
                .Headers
                .ContentType = new MediaTypeHeaderValue("application/json");
            _restHttpClient
                .PostAsync(_restHttpClient.BaseAddress + ResourcePath, body)
                .Result
                .EnsureSuccessStatusCode();
        }

        public void Delete<K>(K Id)
        {
            Delete(GetPathFromType() + Id);
        }

        public void Delete(string ResourcePath)
        {
            _restHttpClient
                .DeleteAsync(_restHttpClient.BaseAddress + ResourcePath)
                .Result
                .EnsureSuccessStatusCode();
        }

        public IList<T> Search<S>(S SearchEntity)
        {
            return Search(GetPathFromType() + SearchEntity.ToQueryString());
        }

        public IList<T> Search(string ResourcePath)
        {
            HttpResponseMessage message = _restHttpClient
                .GetAsync(_restHttpClient.BaseAddress + ResourcePath)
                .Result;
            message.EnsureSuccessStatusCode();
            var content = message
                .Content
                .ReadAsStringAsync()
                .Result;
            return JsonConvert.DeserializeObject<IList<T>>(content);
        }

        public T FindById<K>(K Id)
        {
            return FindById(GetPathFromType() + Id);
        }

        public T FindById(string ResourcePath)
        {
            HttpResponseMessage message = _restHttpClient
                .GetAsync(_restHttpClient.BaseAddress + ResourcePath)
                .Result;
            message.EnsureSuccessStatusCode();
            var content = message
                .Content
                .ReadAsStringAsync()
                .Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void Update<U>(U UpdateEntity)
        {
            Update(GetPathFromType(), UpdateEntity);
        }

        public void Update<U>(string ResourcePath, U UpdateEntity)
        {
            HttpContent body = new StringContent(JsonConvert.SerializeObject(UpdateEntity));
            body
                .Headers
                .ContentType = new MediaTypeHeaderValue("application/json");
            _restHttpClient
                .PutAsync(_restHttpClient.BaseAddress + ResourcePath, body)
                .Result
                .EnsureSuccessStatusCode();
        }

        private string GetPathFromType()
        {
            return typeof(T).Name + @"/";
        }
    }
}