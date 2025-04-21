using Common.DbModels.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory_Service.BusinessLogicLayer.Data.Configurations;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentTypeEntity>
{
    public void Configure(EntityTypeBuilder<DocumentTypeEntity> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.ExternalId).IsUnique();
        builder.HasOne(d => d.EducationLevel)
            .WithMany()
            .HasForeignKey(d => d.EducationLevelId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(d => d.NextEducationLevels)
            .WithMany(e => e.DocumentTypesAsNextLevel)
            .UsingEntity(x => x.ToTable("DocumentTypeNextEducationLevels"));
    }
}