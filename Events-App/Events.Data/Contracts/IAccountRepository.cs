using Events.Data.Contracts;
using Events.Data.Entities;

namespace Events.Data.Contracts
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string loginEmail);
    }
}
