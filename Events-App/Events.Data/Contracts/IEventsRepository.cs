using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Events.Data.Entities;

namespace Events.Data.Contracts
{
    public interface IEventsRepository : IDataRepository<Event>
    {
        IEnumerable<Event> GetAllWithComments(Expression<Func<Event, bool>> filter, int pageIndex, int pageSize);
    }
}
