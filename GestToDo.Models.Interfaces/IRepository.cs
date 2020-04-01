using System;
using System.Collections.Generic;

namespace GestToDo.Models.Interfaces
{
    public interface IRepository<TKey, TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TKey key);
        TEntity Insert(TEntity entity);
        bool Update(TKey key, TEntity entity);
        bool Delete(TKey key);
    }
}
