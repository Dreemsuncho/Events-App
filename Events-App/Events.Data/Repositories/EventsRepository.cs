using System.Collections.Generic;
using Events.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Events.Data.Contracts;
using System;
using System.Linq.Expressions;
using Events.Data.Helpers;
using System.Threading.Tasks;

namespace Events.Data.Repositories
{
    public class EventsRepository : DataRepository<Event>, IDataRepository
    {
        public EventsRepository(EventsDbContext context) : base(context) { }

        public override Event Get(string id)
        {
            return _context.EventSet.Find(id);
        }

        public override PageList<Event> Get(Expression<Func<Event, bool>> filter, int pageIndex, int pageSize)
        {
            var events = _context.EventSet.Where(filter);
            return PageList<Event>.Create(events, pageIndex, pageSize);
        }

        public override IEnumerable<Event> GetAll()
        {
            return _context.EventSet.ToArray();
        }

        public override Event Add(Event entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }

        public override Event Update(Event entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public override void Delete(Event entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
