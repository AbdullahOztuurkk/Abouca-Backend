using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abouca.Domain;
using Microsoft.EntityFrameworkCore;

namespace Abouca.Database.Data
{
    public abstract class EfRepositoryBase<TAggregate,TContext>:IAsyncRepository<TAggregate> 
        where TAggregate:AggregateRoot,new()
        where TContext: DbContext,new()
    {
        public async Task<TAggregate> GetByIdAsync(int id)
        {
            using (TContext context = new TContext())
            {
                var aggregate = await context.Set<TAggregate>().FirstOrDefaultAsync(m => m.Id == id)
                    .ConfigureAwait(false);
                return aggregate;
            }
        }

        public async Task<string> CreateAsync(TAggregate aggregate)
        {
            using (TContext context = new TContext())
            {
                var currentAggregate = context.Entry(aggregate);
                currentAggregate.State = EntityState.Added;
                context.SaveChanges();
                return currentAggregate.Entity.Id.ToString();
            }
        }

        public async Task<TAggregate> RemoveAsync(TAggregate aggregate)
        {
            using (TContext context = new TContext())
            {
                var currentAggregate =  context.Entry(aggregate);
                currentAggregate.State = EntityState.Deleted;
                context.SaveChanges();
                return currentAggregate.Entity;
            }
        }

        public async Task<TAggregate> UpdateAsync(TAggregate aggregate)
        {
            using (TContext context = new TContext())
            {
                var currentAggregate = context.Entry(aggregate);
                currentAggregate.State = EntityState.Modified;
                context.SaveChanges();
                return currentAggregate.Entity;
            }
        }

        public async Task<IEnumerable<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                var currentAggregate = context.Set<TAggregate>().Where(predicate);
                return (IEnumerable<TAggregate>)currentAggregate.ToListAsync();
            }
        }

        public async Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            using (TContext context = new TContext())
            {
                var currentAggregate = context.Set<TAggregate>().SingleOrDefaultAsync(predicate);
                return currentAggregate.Result;
            }
        }
        public async Task<IEnumerable<TAggregate>> GetAllAsync()
        {
            using (TContext context = new TContext())
            {
                return context.Set<TAggregate>().AsQueryable().ToList();
            }
        }
    }
}
