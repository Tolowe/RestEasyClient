using System.Collections.Generic;

namespace RestEasyClient
{
    public interface IRestfulCqrsGateway<T>
    {
        void Create<C>(C CreateEntity);

        void Create<C>(string ResourcePath, C CreateEntity);

        IList<T> Get<S>(S Search);

        IList<T> Get<S>(string ResourcePath, S Search);

        T GetById<K>(K Id);

        T GetById<K>(string ResourcePath, K Id);

        void Update<U>(U UpdateEntity);

        void Update<U>(string ResourcePath, U UpdateEntity);

        void Delete<K>(K Id);

        void Delete<K>(string ResourcePath, K Id);
    }
}