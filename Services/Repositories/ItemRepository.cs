using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services.Repositories;

public class ItemRepository : GenericRepository<Item>
{
    public ItemRepository(EcommerceDbContext context) : base(context) { }
    public override async Task<Item> AddAsync(Item entity)
    {
        await context.Items.AddAsync(entity);
        return entity;
    }
    public override async Task<IEnumerable<Item>> GetAllAsync(int page)
    {
        int normalizedPagination = page - 1;
        const int PAGE_SIZE = 1000;
        return await context.Items
            .Include(i => i.Product)
            .OrderBy(i => i.Id)
            .Skip(normalizedPagination * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();
    }
    public override async Task<IEnumerable<Item>> FindAsync(Expression<Func<Item, bool>> predicate)
    {
        return await context.Items
            .OrderBy(i => i.Id)
            .Where(predicate)
            .ToListAsync();
    }
    public override async Task<Item> GetAsync(long Id)
    {
        return await context.Items
            .Include(i => i.Product)
            .SingleOrDefaultAsync(i => i.Id == Id) ?? 
            throw new ArgumentNullException("Entity not found");
    }
    public override async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    public override async Task<Item> UpdateAsync(Item entity)
    {
        Item item = await context.Items.SingleOrDefaultAsync(i => i.Id == entity.Id) ?? throw new Exception("Entity not found");
        item = entity;
        return await base.UpdateAsync(item);
    }
}

