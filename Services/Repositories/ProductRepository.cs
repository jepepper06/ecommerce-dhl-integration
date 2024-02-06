using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services.Repositories;
public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(EcommerceDbContext context) : base(context) { }
    public override async Task<Product> AddAsync(Product entity)
    {
        await context.Products.AddAsync(entity);
        return entity;
    }
    public override async Task<IEnumerable<Product>> GetAllAsync(int page)
    {
        int normalizedPagination = page - 1;
        const int PAGE_SIZE = 1000;
        return await context.Products
            .OrderBy(p => p.Id)
            .Skip(normalizedPagination * PAGE_SIZE)
            .Take(PAGE_SIZE)
            .ToListAsync();
    }
    public override async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
    {
        return await context.Products
            .OrderBy(p => p.Id)
            .Where(predicate)
            .ToListAsync();
    }
    public override async Task<Product> GetAsync(long Id)
    {
        return await context.Products.SingleOrDefaultAsync(p => p.Id == Id) ?? throw new Exception("Entity not found");
    }
    public override async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
    public override async Task<Product> UpdateAsync(Product entity)
    {
        Product product = await context.Products.SingleOrDefaultAsync(p => p.Id == entity.Id) ?? throw new Exception("Entity not found");
        product = entity;
        return await base.UpdateAsync(product);
    }
}