using Common.DbModels.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory_Service.BusinessLogicLayer.Data.Configurations;

public class EducationProgramConfiguration : IEntityTypeConfiguration<EducationProgramEntity>
{
    public void Configure(EntityTypeBuilder<EducationProgramEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.ExternalId).IsUnique();
    }
}