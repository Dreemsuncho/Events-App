using Events.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventsDbContext _context;
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public UnitOfWork(EventsDbContext context, IDataRepositoryFactory dataRepositoryFactory)
        {
            _context = context;
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public EventsRepository EventsRepository
        {
            get { return new EventsRepository(_context); }
        }

        public AccountRepository AccountRepository
        {
            get { return new AccountRepository(_context); }
        }

        public int Commit()
        {
          return _context.SaveChanges();
        }
    }
}
