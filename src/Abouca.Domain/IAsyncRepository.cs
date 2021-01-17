using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abouca.Domain
{
    public interface IAsyncRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        Task<TAggregate> GetByIdAsync(int id);
        Task<string> CreateAsync(TAggregate aggregate);
        Task<TAggregate> RemoveAsync(TAggregate aggregate);
        Task<TAggregate> UpdateAsync(TAggregate aggregate);
        Task<IEnumerable<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<IEnumerable<TAggregate>> GetAllAsync();
    }
}
