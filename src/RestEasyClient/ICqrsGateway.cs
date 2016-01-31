using System.Collections.Generic;

namespace RestEasyClient
{
    public interface ICqrsGateway<T>
    {
        void Create<C>(C CreateEntity);

        void Create<C>(string ResourcePath, C CreateEntity);

        IList<T> Search<S>(S SearchEntity);

        IList<T> Search(string ResourcePath);

        T FindById<K>(K Id);

        T FindById(string ResourcePath);

        void Update<U>(U UpdateEntity);

        void Update<U>(string ResourcePath, U UpdateEntity);

        void Delete<K>(K Id);

        void Delete(string ResourcePath);
    }
}