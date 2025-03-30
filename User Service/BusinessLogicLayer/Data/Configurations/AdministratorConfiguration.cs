using Common.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User_Service.BusinessLogicLayer.Data.Configurations;

public class AdministratorConfiguration : IEntityTypeConfiguration<AdministratorEntity>
{
    public void Configure(EntityTypeBuilder<AdministratorEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasOne<UserEntity>()
            .WithOne()
            .HasForeignKey<AdministratorEntity>(a => a.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}