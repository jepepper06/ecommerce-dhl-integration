using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;


namespace Services.Repositories;
public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(EcommerceDbContext context) : base(context) { }
    public override async Task<Customer> AddAsync(Customer entity)
    {
        await context.AddAsync(entity);
        return entity;
    }
    public override async Task<IEnumerable<Customer>> GetAllAsync(int page)
    {
        int normalizedPagination = page - 1;
        const int PAGE_SIZE = 1000;
        return await context.Customers
            .OrderBy(c => c.Id)
            .Skip(normalizedPagination * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();
    }
    public override async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await context.Customers
            .OrderBy(c => c.Id)
            .Include(c => c.Orders)
            .Where(predicate)
            .ToListAsync();
    }
    public override async Task<Customer> GetAsync(long Id)
    {
        return await context.Customers.SingleOrDefaultAsync(c => c.Id == Id) ?? throw new ArgumentNullException();
    }
    public override async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    public override async Task<Customer> UpdateAsync(Customer entity)
    {
        // Keep the Update method synchronous
        Customer client = await context.Customers.SingleAsync(e => e.Id == entity.Id);
        client = entity;
        return await base.UpdateAsync(client);
    }
}
