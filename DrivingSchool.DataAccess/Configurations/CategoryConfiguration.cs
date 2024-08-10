using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasMany(с => с.Tests)
                .WithOne(t => t.Category)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameCategory)
                .IsRequired();
        }
    }
}
