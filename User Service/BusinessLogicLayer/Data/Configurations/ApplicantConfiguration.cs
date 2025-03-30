using Common.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User_Service.BusinessLogicLayer.Data.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<ApplicantEntity>
{
    public void Configure(EntityTypeBuilder<ApplicantEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne<UserEntity>()
            .WithOne()
            .HasForeignKey<ApplicantEntity>(a => a.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(a => a.Gender).HasConversion<string>();
    }
}