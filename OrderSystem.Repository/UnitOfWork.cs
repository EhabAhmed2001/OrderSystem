using OrderSystem.Core.Repository;
using OrderSystem.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderSystem.Repository.Data;

namespace OrderSystem.Repository
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _dbContext;
        private Hashtable Repos;

        public UnitOfwork(OrderManagementDbContext DbContext)
        {
            _dbContext = DbContext;
            Repos = new Hashtable();
        }


        public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _dbContext.DisposeAsync();

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!Repos.ContainsKey(type))
            {
                var repository = new GenericRepository<T>(_dbContext);
                Repos.Add(type, repository);
            }
            return (IGenericRepository<T>)Repos[type]!;

        }
    }
}
