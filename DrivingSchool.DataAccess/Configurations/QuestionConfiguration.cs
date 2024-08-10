using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(q => q.Id);

            builder.Property(q => q.QuestionText)
                .IsRequired();

            builder.Property(q => q.Answer1)
                .IsRequired();

            builder.Property(q => q.Answer2)
                .IsRequired();

            builder.Property(q => q.Answer3)
                .IsRequired();

            builder.Property(q => q.Answer4)
                .IsRequired();

            builder.Property(q => q.CorrectAnswer)
                .IsRequired();
        }
    }
}
