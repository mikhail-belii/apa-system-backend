using Common.DbModels.Directory;
using Directory_Service.BusinessLogicLayer.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Directory_Service.BusinessLogicLayer.Data;

public class DirectoryDbContext(DbContextOptions<DirectoryDbContext> options): DbContext(options)
{
    public DbSet<EducationLevelEntity> EducationLevels { get; set; }
    public DbSet<DocumentTypeEntity> DocumentTypes { get; set; }
    public DbSet<FacultyEntity> Faculties { get; set; }
    public DbSet<EducationProgramEntity> EducationPrograms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EducationLevelConfiguration());
        modelBuilder.ApplyConfiguration(new EducationProgramConfiguration());
        modelBuilder.ApplyConfiguration(new FacultyConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}