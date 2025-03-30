using Common.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User_Service.BusinessLogicLayer.Data.Configurations;

public class HeadManagerConfiguration : IEntityTypeConfiguration<HeadManagerEntity>
{
    public void Configure(EntityTypeBuilder<HeadManagerEntity> builder)
    {
        builder.HasKey(h => h.Id);
        builder.HasOne<UserEntity>()
            .WithOne()
            .HasForeignKey<HeadManagerEntity>(h => h.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}