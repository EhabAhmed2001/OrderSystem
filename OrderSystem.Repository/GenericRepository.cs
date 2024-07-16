using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Repository;
using OrderSystem.Core.Specofication;
using OrderSystem.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly OrderManagementDbContext _dbContext;

        public GenericRepository(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
        => await _dbContext.Set<T>().AddAsync(item);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _dbContext.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        => await GenerateSpec(Spec).ToListAsync();

        public async Task<T?> GetByIdWithSpecAsync(ISpecifications<T> Spec)
        => await GenerateSpec(Spec).FirstOrDefaultAsync();


        private IQueryable<T> GenerateSpec(ISpecifications<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec).Result;
        }
    }
}
