using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork Uow;
        
        public BaseBLL(TUnitOfWork uow)
        {
            Uow = uow;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await Uow.SaveChangesAsync();
        }
        
        private readonly Dictionary<Type, object> _serviceCache = new();

        public TService GetService<TService>(Func<TService> serviceCreatingMethod) 
            where TService : class
        {
            if (_serviceCache.TryGetValue(typeof(TService), out var service))
            {
                return (TService) service;
            }

            var serviceInstance = serviceCreatingMethod();
            _serviceCache.Add(typeof(TService), serviceInstance);
            return serviceInstance;
        }
    }
}