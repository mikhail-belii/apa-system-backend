using Common.DbModels.Users;
using Microsoft.EntityFrameworkCore;
using User_Service.BusinessLogicLayer.Data.Configurations;

namespace User_Service.BusinessLogicLayer.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ApplicantEntity> Applicants { get; set; }
    public DbSet<ManagerEntity> Managers { get; set; }
    public DbSet<HeadManagerEntity> HeadManagers { get; set; }
    public DbSet<AdministratorEntity> Administrators { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new HeadManagerConfiguration());
        modelBuilder.ApplyConfiguration(new AdministratorConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}