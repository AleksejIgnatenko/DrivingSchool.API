using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasMany(t => t.Questions)
                .WithOne(q => q.Test)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.AnswerUserTests)
                .WithOne(a => a.Test)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.NameTest)
                    .IsRequired();
        }
    }
}
