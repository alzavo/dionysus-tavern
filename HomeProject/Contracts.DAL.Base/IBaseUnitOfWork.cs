using System;
using System.Threading.Tasks;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();

        TRepository GetRepository<TRepository>(Func<TRepository> repositoryCreatingMethod)
            where TRepository : class;
    }
}