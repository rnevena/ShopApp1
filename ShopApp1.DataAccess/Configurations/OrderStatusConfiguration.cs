using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.DataAccess.Configurations
{
    public class OrderStatusConfiguration : EntityConfiguration<OrderStatus>
    {
        protected override void ConfigureRules(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasMany(x => x.Orders).WithOne(x => x.OrderStatus).HasForeignKey(x => x.OrderStatusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
