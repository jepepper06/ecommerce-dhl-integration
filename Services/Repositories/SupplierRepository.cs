using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services.Repositories;
public class SupplierRepository : GenericRepository<Supplier>
{
    public SupplierRepository(EcommerceDbContext context) : base(context) { }
    public override async Task<Supplier> AddAsync(Supplier entity)
    {
        await context.Suppliers.AddAsync(entity);
        return entity;
    }
    public override async Task<IEnumerable<Supplier>> GetAllAsync(int page)
    {
        int normalizedPagination = page - 1;
        const int PAGE_SIZE = 1000;
        return await context.Suppliers
            .OrderBy(s => s.Id)
            .Skip(normalizedPagination * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();
    }
    public override async Task<IEnumerable<Supplier>> FindAsync(Expression<Func<Supplier, bool>> predicate)
    {
        return await context.Suppliers
            .OrderBy(s => s.Id)
            .Where(predicate)
            .ToListAsync();
    }
    public override async Task<Supplier> GetAsync(long Id)
    {
        return await context.Suppliers.SingleOrDefaultAsync(s => s.Id == Id) ?? throw new Exception("Entity not found");
    }
    public override async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    public override async Task<Supplier> UpdateAsync(Supplier entity)
    {
        Supplier supplier = await context.Suppliers.SingleOrDefaultAsync(e => e.Id == entity.Id) ?? throw new Exception("Entity not found");
        supplier = entity;
        return await base.UpdateAsync(supplier);
    }
}