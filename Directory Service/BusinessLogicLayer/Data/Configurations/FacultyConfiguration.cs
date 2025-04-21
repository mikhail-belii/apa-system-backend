using Common.DbModels.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory_Service.BusinessLogicLayer.Data.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<FacultyEntity>
{
    public void Configure(EntityTypeBuilder<FacultyEntity> builder)
    {
        builder.HasKey(f => f.Id);
        builder.HasIndex(f => f.ExternalId).IsUnique();
    }
}