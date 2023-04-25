using EcommerceShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Data.Configurations
{
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductsInCategories");

            builder.HasKey(t => new {t.CategoryId, t.ProductId});

            builder.HasOne(t => t.Product).WithMany(p => p.ProductsInCategories)
                .HasForeignKey(p => p.ProductId);
            builder.HasOne(t => t.Category).WithMany(p => p.ProductsInCategories)
                .HasForeignKey(p => p.CategoryId);

        }
    }
}
