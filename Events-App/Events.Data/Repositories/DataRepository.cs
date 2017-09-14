using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Events.Data.Contracts;
using Events.Data.Entities;

namespace Events.Data.Repositories
{
    public abstract class DataRepository<IEntity> : IDataRepository<IEntity>, IDataRepository where IEntity : class
    {
        protected readonly EventsDbContext _context;

        public DataRepository() { }
        public DataRepository(EventsDbContext context)
        {
            this._context = context;
        }


        #region IDataRepository members

        public IEntity Get(Guid id)
        {
            return (IEntity)_context.Find(typeof(IEntity), id);
        }

        public IEnumerable<IEntity> GetAll(Expression<Func<IEntity, bool>> filter)
        {
            IEnumerable<IEntity> result = null;

            if (typeof(IEntity) == typeof(Event))
                result = (IEnumerable<IEntity>)_context.EventSet.Where(filter as Expression<Func<Event, bool>>);

            if (typeof(IEntity) == typeof(Account))
                result = (IEnumerable<IEntity>)_context.AccountSet.Where(filter as Expression<Func<Account, bool>>);

            if (typeof(IEntity) == typeof(Comment))
                result = (IEnumerable<IEntity>)_context.CommentSet.Where(filter as Expression<Func<Comment, bool>>);

            return result;
        }

        public IEnumerable<IEntity> GetAll()
        {
            IEnumerable<IEntity> result = null;

            if (typeof(IEntity) == typeof(Event))
                result = (IEnumerable<IEntity>)_context.EventSet;

            if (typeof(IEntity) == typeof(Account))
                result = (IEnumerable<IEntity>)_context.AccountSet;

            if (typeof(IEntity) == typeof(Comment))
                result = (IEnumerable<IEntity>)_context.CommentSet;

            return result;
        }

        public IEntity Add(IEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }

        public IEntity Update(IEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public void Delete(IEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        #endregion
    }
}
