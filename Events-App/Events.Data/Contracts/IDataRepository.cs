using Events.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Events.Data.Contracts
{
    public interface IDataRepository { }

    public interface IDataRepository<IEntity> : IDataRepository where IEntity : class
    {
        IEntity Get(Guid id);
        IEnumerable<IEntity> GetAll();
        IEnumerable<IEntity> GetAll(Expression<Func<IEntity, bool>> filter);
        IEntity Add(IEntity entity);
        IEntity Update(IEntity entity);
        void Delete(IEntity entity);
    }
}