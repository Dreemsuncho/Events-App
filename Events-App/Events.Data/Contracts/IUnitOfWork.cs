using Events.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.Data.Contracts
{
    public interface IUnitOfWork
    {
        IEventsRepository EventsRepository { get; }
        IAccountRepository AccountRepository { get; }
        int Commit();
    }
}
