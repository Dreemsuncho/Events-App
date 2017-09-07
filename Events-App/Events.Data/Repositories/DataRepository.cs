using Events.Data.Contracts;
using Events.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data.Repositories
{
    public abstract class DataRepository<T> : IDataRepository<T> where T : IEntity
    {
        protected readonly EventsDbContext _context;

        public DataRepository(EventsDbContext context)
        {
            this._context = context;
        }

        public abstract T Get(string id);
        public abstract PageList<T> Get(Expression<Func<T, bool>> filter, int pageIndex,int pageSize);
        public abstract IEnumerable<T> GetAll();
        public abstract T Add(T entity);
        public abstract T Update(T entity);
        public abstract void Delete(T entity);
    }
}
