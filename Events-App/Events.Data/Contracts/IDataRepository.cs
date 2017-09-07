using Events.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Events.Data.Contracts
{
    public interface IDataRepository { }

    public interface IDataRepository<T> : IDataRepository where T : IEntity
    {
        T Get(string id);
        PageList<T> Get(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}