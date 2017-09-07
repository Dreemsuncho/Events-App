namespace Events.Data.Contracts
{
    public interface IDataRepositoryFactory
    {
        T GetRepository<T>() where T : IDataRepository;
    }
}