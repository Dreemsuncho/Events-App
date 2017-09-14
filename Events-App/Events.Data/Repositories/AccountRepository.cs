using System.Linq;
using Events.Data.Contracts;
using Events.Data.Entities;

namespace Events.Data.Repositories
{
    public class AccountRepository : DataRepository<Account>, IAccountRepository, IDataRepository
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
