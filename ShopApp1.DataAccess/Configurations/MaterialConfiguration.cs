using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.DataAccess.Configurations
{
    public class MaterialConfiguration : EntityConfiguration<Material>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Material> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.ProductMaterials).WithOne(x => x.Material).HasForeignKey(x => x.MaterialId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
