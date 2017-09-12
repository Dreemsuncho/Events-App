using Events.Data.Contracts;
using Events.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events.Data.Repositories
{
    public class AccountRepository : DataRepository<Account>, IDataRepository
    {
        public AccountRepository() { }
        public AccountRepository(EventsDbContext context) : base(context)
        {

        }

        public Account GetByLogin(string loginEmail)
        {
            return _context.AccountSet.FirstOrDefault(a => a.UserName == loginEmail);
        }
    }
}
