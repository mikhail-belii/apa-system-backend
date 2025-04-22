using Common.DbModels.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory_Service.BusinessLogicLayer.Data.Configurations;

public class DirectoryImportLogConfiguration : IEntityTypeConfiguration<DirectoryImportLogEntity>
{
    public void Configure(EntityTypeBuilder<DirectoryImportLogEntity> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Status).HasConversion<string>();
        builder.Property(d => d.DirectoryType).HasConversion<string>();
    }
}