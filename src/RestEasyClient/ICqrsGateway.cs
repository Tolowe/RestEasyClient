using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestEasyClient
{
    public interface ICqrsGateway<T>
    {
        void Create<C>(C CreateEntity);

        Task CreateAsync<C>(C CreateEntity);

        void Create<C>(string ResourcePath, C CreateEntity);

        Task CreateAsync<C>(string ResourcePath, C CreateEntity);

        IList<T> Search<S>(S SearchEntity);

        Task<IList<T>> SearchAsync<S>(S SearchEntity);

        IList<T> Search(string ResourcePath);

        Task<IList<T>> SearchAsync(string ResourcePath);

        T FindById<K>(K Key);

        Task<T> FindByIdAsync<K>(K Key);

        T FindById(string ResourcePath);

        Task<T> FindByIdAsync(string ResourcePath);

        void Update<K, U>(K Key, U UpdateEntity);

        Task UpdateAsync<K, U>(K Key, U UpdateEntity);

        void Update<U>(string ResourcePath, U UpdateEntity);

        Task UpdateAsync<U>(string ResourcePath, U UpdateEntity);

        void Delete<K>(K Key);

        Task DeleteAsync<K>(K Key);

        void Delete(string ResourcePath);

        Task DeleteAsync(string ResourcePath);
    }
}