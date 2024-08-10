using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasMany(u => u.AnswerUserTests)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                    .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired();
        }
    }
}