using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WareHouse.Entity.Domain;

namespace WareHouse.DataAccess.Configuration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedNever();
            //builder.Property(a => a.CategoryName).HasMaxLength(255).IsRequired();
            //builder.HasIndex(a => a.CategoryName).IsUnique();
        }
    }
}