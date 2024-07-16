using OrderSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        Task<int> CompleteAsync();
        IGenericRepository<T> Repository<T>() where T : class;
    }
}
