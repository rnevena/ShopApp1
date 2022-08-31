using Microsoft.EntityFrameworkCore;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.DataAccess
{
    public class ShopApp1Context : DbContext
    {
        public IApplicationUser User { get; }
        public ShopApp1Context(IApplicationUser user = null)
        {
            User = user;
        }
        public ShopApp1Context() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<ProductMaterial>().HasKey(x => new { x.ProductId, x.MaterialId });
            modelBuilder.Entity<OrderLine>().HasKey(x => new { x.ProductId, x.OrderId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UserUseCaseId });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ShopApp;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            if (entry.Entity is Order o)
                            {
                                o.OrderDate = DateTime.UtcNow;
                            }
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User?.Identity;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        
    }
}
