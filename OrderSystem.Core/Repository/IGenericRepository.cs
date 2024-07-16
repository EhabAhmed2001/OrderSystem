using OrderSystem.Core.Specofication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Core.Repository
{
    public interface IGenericRepository<T>
    {
        #region Without Specification

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        #endregion

        #region With Specification

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
        Task<T?> GetByIdWithSpecAsync(ISpecifications<T> Spec);

        #endregion

        Task AddAsync(T item);
    }
}
