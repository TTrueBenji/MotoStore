using System.Collections.Generic;

namespace MotoStore.Repositories.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Create(T item);
        void Update(T item);
        void DeleteById(string id);
    }
}