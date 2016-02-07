using Newtonsoft.Json;
using RestEasyClient.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestEasyClient.Impl
{
    public class CqrsGateway<T> : ICqrsGateway<T>
    {
        private readonly HttpClient _httpClient;

        public CqrsGateway(HttpClient HttpClient)
        {
            _httpClient = HttpClient;
        }

        public void Create<C>(C CreateEntity)
        {
            CreateAsync(CreateEntity).Wait();
        }

        public void Create<C>(string ResourcePath, C CreateEntity)
        {
            CreateAsync(ResourcePath, CreateEntity).Wait();
        }

        public void Delete<K>(K Key)
        {
            DeleteAsync(Key).Wait();
        }

        public void Delete(string ResourcePath)
        {
            DeleteAsync(ResourcePath).Wait();
        }

        public IList<T> Search<S>(S SearchEntity)
        {
            return SearchAsync(SearchEntity).Result;
        }

        public IList<T> Search(string ResourcePath)
        {
            return SearchAsync(ResourcePath).Result;
        }

        public T FindById<K>(K Key)
        {
            return FindByIdAsync(Key).Result;
        }

        public T FindById(string ResourcePath)
        {
            return FindByIdAsync(ResourcePath).Result;
        }

        public void Update<K, U>(K Key, U UpdateEntity)
        {
            UpdateAsync(Key, UpdateEntity).Wait();
        }

        public void Update<U>(string ResourcePath, U UpdateEntity)
        {
            UpdateAsync(ResourcePath, UpdateEntity).Wait();
        }

        private string GetPathFromType()
        {
            return typeof(T).Name + @"/";
        }

        public Task CreateAsync<C>(C CreateEntity)
        {
            return CreateAsync(GetPathFromType(), CreateEntity);
        }

        public async Task CreateAsync<C>(string ResourcePath, C CreateEntity)
        {
            HttpContent body = new StringContent(JsonConvert.SerializeObject(CreateEntity));
            body
                .Headers
                .ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _httpClient
                .PostAsync(_httpClient.BaseAddress + ResourcePath, body);
            result.EnsureSuccessStatusCode();
        }

        public Task<IList<T>> SearchAsync<S>(S SearchEntity)
        {
            var resourceRoute = (GetPathFromType()
                .Remove(GetPathFromType()
                    .Length - 1)) + SearchEntity.ToQueryString();
            return SearchAsync(resourceRoute);
        }

        public async Task<IList<T>> SearchAsync(string ResourcePath)
        {
            HttpResponseMessage message = await _httpClient
                .GetAsync(_httpClient.BaseAddress + ResourcePath);
            message.EnsureSuccessStatusCode();
            var content = await message
                .Content
                .ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IList<T>>(content);
        }

        public Task<T> FindByIdAsync<K>(K Key)
        {
            return FindByIdAsync(GetPathFromType() + Key);
        }

        public async Task<T> FindByIdAsync(string ResourcePath)
        {
            HttpResponseMessage message = await _httpClient
                .GetAsync(_httpClient.BaseAddress + ResourcePath);
            message.EnsureSuccessStatusCode();
            var content = await message
                .Content
                .ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public Task UpdateAsync<K, U>(K Key, U UpdateEntity)
        {
            return UpdateAsync(GetPathFromType() + Key, UpdateEntity);
        }

        public async Task UpdateAsync<U>(string ResourcePath, U UpdateEntity)
        {
            HttpContent body = new StringContent(JsonConvert.SerializeObject(UpdateEntity));
            body
                .Headers
                .ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _httpClient
                .PutAsync(_httpClient.BaseAddress + ResourcePath, body);
            result.EnsureSuccessStatusCode();
        }

        public Task DeleteAsync<K>(K Key)
        {
            return DeleteAsync(GetPathFromType() + Key);
        }

        public async Task DeleteAsync(string ResourcePath)
        {
            var result = await _httpClient
                .DeleteAsync(_httpClient.BaseAddress + ResourcePath);
            result.EnsureSuccessStatusCode();
        }
    }
}