using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RestEasyClient.Impl
{
    public class RestfulCqrsGateway<T> : IRestfulCqrsGateway<T>
    {
        private readonly HttpClient _httpClient;

        public RestfulCqrsGateway(string ProtocolAndDomain)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ProtocolAndDomain);
        }

        public void Create<C>(C CreateEntity)
        {
            Create(GetPathFromType(), CreateEntity);
        }

        public void Create<C>(string ResourcePath, C CreateEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete<K>(K Id)
        {
            Delete(GetPathFromType(), Id);
        }

        public void Delete<K>(string ResourcePath, K Id)
        {
            throw new NotImplementedException();
        }

        public IList<T> Get<S>(S Search)
        {
            return Get(GetPathFromType(), Search);
        }

        public IList<T> Get<S>(string ResourcePath, S Search)
        {
            throw new NotImplementedException();
        }

        public T GetById<K>(K Id)
        {
            return GetById(GetPathFromType(), Id);
        }

        public T GetById<K>(string ResourcePath, K Id)
        {
            throw new NotImplementedException();
        }

        public void Update<U>(U UpdateEntity)
        {
            Update(GetPathFromType(), UpdateEntity);
        }

        public void Update<U>(string ResourcePath, U UpdateEntity)
        {
            throw new NotImplementedException();
        }

        private string GetPathFromType()
        {
            return typeof(T).Name;
        }
    }
}