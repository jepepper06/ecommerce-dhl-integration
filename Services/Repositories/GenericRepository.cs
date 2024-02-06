using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Services.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected EcommerceDbContext context;
    public GenericRepository(EcommerceDbContext context)
    {
        this.context = context;
    }
    public virtual async Task<T> AddAsync(T entity)
    {
        await context.AddAsync(entity);
        return entity;
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber)
    {
        return await context.Set<T>().ToListAsync();
    }
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().AsQueryable().Where(predicate).ToListAsync();
    }
    public virtual async Task<T> GetAsync(long Id)
    {
        return await context.FindAsync<T>(Id) ?? throw new Exception("Entity not found");
    }
    public virtual async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    public virtual async Task<T> UpdateAsync(T entity)
    {            
        context.Update(entity);
        return entity;
    }
    public virtual async Task DeleteAsync(long Id)
    {
        var entity = await context.Set<T>().FindAsync(Id) ??
            throw new Exception("Entity not found");
        context.Set<T>().Remove(entity);
    }
}