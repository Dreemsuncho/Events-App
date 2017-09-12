using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Events.Data.Entities;
using Events.Data.Contracts;
using Events.Data.Helpers;

namespace Events.Data.Repositories
{
    public class EventsRepository : DataRepository<Event>, IDataRepository
    {
        public EventsRepository() { }
        public EventsRepository(EventsDbContext context) : base(context) { }

        public virtual IEnumerable<Event> GetAllWithComments(Expression<Func<Event, bool>> filter, int pageIndex, int pageSize)
        {
            var events = _context.EventSet
                .Include(e => e.Author)
                .Include(e => e.Comments)
                .ThenInclude(p => p.Author)
                .Where(filter);

            return PageList<Event>.Create(events, pageIndex, pageSize).AsEnumerable();
        }
    }
}
