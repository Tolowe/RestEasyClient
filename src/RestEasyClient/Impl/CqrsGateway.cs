using Newtonsoft.Json;
using RestEasyClient.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestEasyClient.Impl
{
    public class CqrsGateway<T> : ICqrsGateway<T>
    {
        private readonly HttpClient _httpClient;

        public CqrsGateway(string ProtocolAndDomain)
        {
            _httpClient = new HttpClient();
            var baseUri = (ProtocolAndDomain.EndsWith(@"/") ? ProtocolAndDomain : ProtocolAndDomain + @"/");
            _httpClient.BaseAddress = new Uri(baseUri);
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
            _httpClient
                .PostAsync(_httpClient.BaseAddress + ResourcePath, body)
                .Result
                .EnsureSuccessStatusCode();
        }

        public void Delete<K>(K Id)
        {
            Delete(GetPathFromType() + Id);
        }

        public void Delete(string ResourcePath)
        {
            _httpClient
                .DeleteAsync(_httpClient.BaseAddress + ResourcePath)
                .Result
                .EnsureSuccessStatusCode();
        }

        public IList<T> Search<S>(S SearchEntity)
        {
            return Search(GetPathFromType() + SearchEntity.ToQueryString());
        }

        public IList<T> Search(string ResourcePath)
        {
            HttpResponseMessage message = _httpClient
                .GetAsync(_httpClient.BaseAddress + ResourcePath)
                .Result;
            message.EnsureSuccessStatusCode();
            var content = message
                .Content
                .ReadAsStringAsync()
                .Result;
            return JsonConvert.DeserializeObject<IList<T>>(content);
        }

        public T GetById<K>(K Id)
        {
            return GetById(GetPathFromType() + Id);
        }

        public T GetById(string ResourcePath)
        {
            HttpResponseMessage message = _httpClient
                .GetAsync(_httpClient.BaseAddress + ResourcePath)
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
            _httpClient
                .PutAsync(_httpClient.BaseAddress + ResourcePath, body)
                .Result
                .EnsureSuccessStatusCode();
        }

        private string GetPathFromType()
        {
            return typeof(T).Name + @"/";
        }
    }
}