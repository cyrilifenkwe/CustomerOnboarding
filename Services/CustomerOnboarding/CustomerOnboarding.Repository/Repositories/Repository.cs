using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using CustomerOnboarding.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repositories.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly CustomerOnboardingContext context;
        public Repository(CustomerOnboardingContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return context.Set<T>().AsNoTracking();
        }

        public async Task<IQueryable<T>> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).AsNoTracking();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

    }
}
