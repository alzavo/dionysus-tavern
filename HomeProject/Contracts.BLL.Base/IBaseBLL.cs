using System;
using System.Threading.Tasks;

namespace Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();

        TService GetService<TService>(Func<TService> serviceCreatingMethod)
            where TService : class;
    }
}
