using Microsoft.EntityFrameworkCore;
using Model;

namespace Services;
public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions options) : base(options){}
    public EcommerceDbContext() : base() { }
    public DbSet<Customer> Customers{ get; set; }
    public DbSet<Item> Items{ get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured){
            optionsBuilder.UseNpgsql("User Id=postgres;Password=root;Server=localhost;Port=5432;Database=DhlEcommerceIntegration;");
        }
        // base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>(customer => {
            customer.HasKey(e => e.Id);
            customer.HasMany(e => e.Orders)
                .WithOne(o => o.Customer);
            customer.Property(e => e.Document)
                .IsRequired();
            customer.Property(e => e.Email)
                .IsRequired();
            customer.Property(e => e.Name)
                .IsRequired();
            customer.Property(e => e.PhoneNumber)
                .IsRequired();
        });
    
        modelBuilder.Entity<Item>(item => {
            item.HasKey(e => e.Id);
            item.HasOne(e => e.Product);
            item.Property(e => e.Quantity)
                .IsRequired();
        });

        modelBuilder.Entity<Order>(order => {
            order.HasKey(e => e.Id);
            order.HasOne(e => e.Customer);
            order.Property(e => e.Total)
                .IsRequired();
            order.HasOne(e => e.Customer); 
            order.HasMany(e => e.Items)
                .WithOne(i => i.Order);
        });

        modelBuilder.Entity<Product>(product => {
            product.HasKey(e => e.Id);
            product.HasOne(p => p.Supplier);
            product.Property(p => p.Name)
                .IsRequired();
            product.Property(p => p.Name)
                .IsRequired();
            product.Property(p => p.Path)
                .IsRequired();
            product.Property(p => p.Price)
                .IsRequired();
        });

        modelBuilder.Entity<Supplier>(supplier => {
            supplier.HasKey(e => e.Id);
            supplier.Property(e => e.BaseUrl)
                .IsRequired();
            supplier.Property(e => e.Description)
                .IsRequired();
            supplier.Property(e => e.Name)
                .IsRequired();
        });
    }
}
