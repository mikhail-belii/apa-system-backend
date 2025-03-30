using Common.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User_Service.BusinessLogicLayer.Data.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<ApplicantEntity>
{
    public void Configure(EntityTypeBuilder<ApplicantEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Gender).HasConversion<string>();
    }
}