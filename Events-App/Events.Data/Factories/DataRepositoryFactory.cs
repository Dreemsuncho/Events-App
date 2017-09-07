using System;
using Events.Data.Contracts;

namespace Events.Data.Factories
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DataRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetRepository<T>() where T : IDataRepository
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
