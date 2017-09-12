using Events.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.Data.Contracts
{
    public interface IUnitOfWork
    {
        EventsRepository EventsRepository { get; }
        AccountRepository AccountRepository { get; }
        int Commit();
    }
}
