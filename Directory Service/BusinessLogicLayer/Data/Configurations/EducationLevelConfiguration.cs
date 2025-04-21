using Common.DbModels.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory_Service.BusinessLogicLayer.Data.Configurations;

public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevelEntity>
{
    public void Configure(EntityTypeBuilder<EducationLevelEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.ExternalId).IsUnique();
    }
}