using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.DataAccess.Configurations
{
    public class AnswerUserTestConfiguration : IEntityTypeConfiguration<AnswerUserTestEntity>
    {
        public void Configure(EntityTypeBuilder<AnswerUserTestEntity> builder)
        {
            builder.HasOne(a => a.Test)
                .WithMany(t => t.AnswerUserTests)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.User)
                .WithMany(u => u.AnswerUserTests)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.ResultTest)
                .IsRequired();
        }
    }
}
