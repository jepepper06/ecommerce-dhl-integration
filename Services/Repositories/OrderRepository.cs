using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services.Repositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(EcommerceDbContext context) : base(context) { }
    public override async Task<Order> AddAsync(Order entity)
    {
        await context.Orders.AddAsync(entity);
        return entity;
    }
    public override async Task<IEnumerable<Order>> GetAllAsync(int page)
    {
        int normalizedPagination = page - 1;
        const int PAGE_SIZE = 1000;
        return await context.Orders
            .OrderBy(o => o.Id)
            .Include(o => o.Customer)
            .Skip(normalizedPagination * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();
    }
    public override async Task<IEnumerable<Order>> FindAsync(Expression<Func<Order, bool>> predicate)
    {
        return await context.Orders
            .OrderBy(o => o.Id)
            .Where(predicate)
            .ToListAsync();
    }
    public override async Task<Order> GetAsync(long Id)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .SingleAsync(o => o.Id == Id) ?? throw new Exception("Entity not found");
    }
    public override async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    public override async Task<Order> UpdateAsync(Order entity)
    {
        Order order = await context.Orders.SingleAsync(e => e.Id == entity.Id);
        order = entity;
        return await base.UpdateAsync(order);
    }
}

