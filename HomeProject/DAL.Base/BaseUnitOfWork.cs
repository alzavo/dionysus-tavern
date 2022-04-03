using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork : IBaseUnitOfWork
    {
        public abstract Task<int> SaveChangesAsync();  
        
        private readonly Dictionary<Type, object> _repositoryCache = new();

        public TRepository GetRepository<TRepository>(Func<TRepository> repositoryCreatingMethod)
            where TRepository : class
        {
            if (_repositoryCache.TryGetValue(typeof(TRepository), out var repository))
            {
                return (TRepository) repository;
            }

            var repositoryInstance = repositoryCreatingMethod();
            _repositoryCache.Add(typeof(TRepository), repositoryInstance);
            return repositoryInstance;
        }
    }
}