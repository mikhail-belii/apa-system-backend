using Common.DbModels.Directory;
using Common.DtoModels.Directory;
using Common.DtoModels.Enums;
using Common.DtoModels.Other;
using Common.Interfaces.DirectoryService;
using Directory_Service.BusinessLogicLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Directory_Service.BusinessLogicLayer.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IExternalApiService _externalApiService;
    private readonly DirectoryDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public DirectoryService(IExternalApiService externalApiService, DirectoryDbContext dbContext, IConfiguration configuration)
    {
        _externalApiService = externalApiService;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<ResponseModel> ImportDirectory(ImportDirectoryModel importDirectoryModel)
    {
        var now = DateTime.UtcNow;

        foreach (var type in importDirectoryModel.DirectoryTypes)
        {
            switch (type)
            {
                case DirectoryType.EducationLevels:
                    var levels = await _externalApiService.ImportEducationLevelsAsync();
                    var dbLevels = await _dbContext.EducationLevels.ToListAsync();
                    foreach (var level in levels)
                    {
                        var existing = dbLevels.FirstOrDefault(
                            e => e.ExternalId == level.Id && e.IsActual);
                        if (existing != null)
                        {
                            if (existing.Name != level.Name)
                            {
                                existing.IsActual = false;
                                existing.LastUpdated = now;
                                _dbContext.Update(existing);

                                var newEducationLevel = new EducationLevelEntity
                                {
                                    CreateTime = now,
                                    ExternalId = level.Id,
                                    Name = level.Name,
                                    Id = Guid.NewGuid(),
                                    IsActual = true
                                };
                                _dbContext.EducationLevels.Add(newEducationLevel);
                            }
                        }
                        else
                        {
                            var newEducationLevel = new EducationLevelEntity
                            {
                                CreateTime = now,
                                ExternalId = level.Id,
                                Name = level.Name,
                                Id = Guid.NewGuid(),
                                IsActual = true
                            };
                            _dbContext.EducationLevels.Add(newEducationLevel);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    break;
                case DirectoryType.DocumentTypes:
                    var types = await _externalApiService.ImportDocumentTypesAsync();
                    var dbTypes = await _dbContext.DocumentTypes
                        .Include(d => d.EducationLevel)
                        .Include(d => d.NextEducationLevels)
                        .ToListAsync();
                    foreach (var documentType in types)
                    {
                        var existing = dbTypes.FirstOrDefault(
                            d => d.ExternalId == documentType.Id && d.IsActual);
                        if (existing != null)
                        {
                            if (existing.Name != documentType.Name)
                            {
                                existing.IsActual = false;
                                existing.LastUpdated = now;
                                _dbContext.Update(existing);
                                
                                var actualEducationLevels = await _dbContext.EducationLevels
                                    .Where(e => e.IsActual)
                                    .ToListAsync();
                                var educationLevelId = await _dbContext.EducationLevels
                                    .Where(e => e.ExternalId == documentType.EducationLevel.Id && e.IsActual)
                                    .Select(e => e.Id)
                                    .FirstAsync();

                                var newDocumentType = new DocumentTypeEntity
                                {
                                    CreateTime = now,
                                    EducationLevelId = educationLevelId,
                                    ExternalId = documentType.Id,
                                    Id = Guid.NewGuid(),
                                    IsActual = true,
                                    Name = documentType.Name,
                                    NextEducationLevels = documentType.NextEducationLevels?
                                        .Select(level => actualEducationLevels
                                            .FirstOrDefault(e => e.ExternalId == level.Id))
                                        .Where(e => e != null)
                                        .ToList()
                                };
                                _dbContext.DocumentTypes.Add(newDocumentType);
                            }
                        }
                        else
                        {
                            var actualEducationLevels = await _dbContext.EducationLevels
                                .Where(e => e.IsActual)
                                .ToListAsync();
                            var educationLevelId = await _dbContext.EducationLevels
                                .Where(e => e.ExternalId == documentType.EducationLevel.Id && e.IsActual)
                                .Select(e => e.Id)
                                .FirstAsync();

                            var newDocumentType = new DocumentTypeEntity
                            {
                                CreateTime = now,
                                EducationLevelId = educationLevelId,
                                ExternalId = documentType.Id,
                                Id = Guid.NewGuid(),
                                IsActual = true,
                                Name = documentType.Name,
                                NextEducationLevels = documentType.NextEducationLevels?
                                    .Select(level => actualEducationLevels
                                        .FirstOrDefault(e => e.ExternalId == level.Id))
                                    .Where(e => e != null)
                                    .ToList()
                            };
                            _dbContext.DocumentTypes.Add(newDocumentType);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    break;
                case DirectoryType.Faculties:
                    var faculties = await _externalApiService.ImportFacultiesAsync();
                    var dbFaculties = await _dbContext.Faculties.ToListAsync();
                    foreach (var faculty in faculties)
                    {
                        var existing = dbFaculties.FirstOrDefault(
                            f => f.ExternalId == faculty.Id && f.IsActual);
                        if (existing != null)
                        {
                            if (existing.Name != faculty.Name)
                            {
                                existing.IsActual = false;
                                existing.LastUpdated = now;
                                _dbContext.Update(existing);

                                var newFaculty = new FacultyEntity
                                {
                                    CreateTime = now,
                                    ExternalId = faculty.Id,
                                    Id = Guid.NewGuid(),
                                    IsActual = true,
                                    Name = faculty.Name
                                };
                                _dbContext.Faculties.Add(newFaculty);
                            }
                        }
                        else
                        {
                            var newFaculty = new FacultyEntity
                            {
                                CreateTime = now,
                                ExternalId = faculty.Id,
                                Id = Guid.NewGuid(),
                                IsActual = true,
                                Name = faculty.Name
                            };
                            _dbContext.Faculties.Add(newFaculty);
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                    break;
                case DirectoryType.Programs:
                    var pageSize = _configuration
                        .GetSection("ImportSettings:ProgramsPagination:PageSize")
                        .Get<int>();
                    var currentPage = 1;
                    var pageInfoModel = await _externalApiService.GetProgramsPageInfoModel(pageSize);
                    var count = pageInfoModel.Count;
                    var dbPrograms = await _dbContext.EducationPrograms
                        .Include(p => p.EducationLevel)
                        .Include(p => p.Faculty)
                        .Where(p => p.IsActual)
                        .ToListAsync();
                    while (currentPage <= count)
                    {
                        var programs = await _externalApiService.ImportProgramsAsync(currentPage++, pageSize);
                        foreach (var program in programs)
                        {
                            var existing = dbPrograms.FirstOrDefault(
                                p => p.ExternalId == program.Id);
                            if (existing != null)
                            {
                                if (existing.Name != program.Name ||
                                    existing.Code != program.Code ||
                                    existing.Language != program.Language ||
                                    existing.EducationForm != program.EducationForm ||
                                    existing.Faculty.ExternalId != program.Faculty.Id ||
                                    existing.EducationLevel.ExternalId != program.EducationLevel.Id)
                                {
                                    existing.IsActual = false;
                                    existing.LastUpdated = now;
                                    _dbContext.EducationPrograms.Update(existing);

                                    var newProgram = new EducationProgramEntity
                                    {
                                        Id = Guid.NewGuid(),
                                        CreateTime = now,
                                        ExternalId = program.Id,
                                        Code = program.Code,
                                        Name = program.Name,
                                        Language = program.Language,
                                        EducationForm = program.EducationForm,
                                        Faculty = await _dbContext.Faculties
                                            .FirstAsync(
                                                f => f.ExternalId == program.Faculty.Id && f.IsActual),
                                        EducationLevel = await _dbContext.EducationLevels
                                            .FirstAsync(
                                                l => l.ExternalId == program.EducationLevel.Id && l.IsActual),
                                        IsActual = true
                                    };
                                    _dbContext.EducationPrograms.Add(newProgram);
                                }
                            }
                            else
                            {
                                var newProgram = new EducationProgramEntity
                                {
                                    Id = Guid.NewGuid(),
                                    CreateTime = now,
                                    ExternalId = program.Id,
                                    Code = program.Code,
                                    Name = program.Name,
                                    Language = program.Language,
                                    EducationForm = program.EducationForm,
                                    Faculty = await _dbContext.Faculties
                                        .FirstAsync(
                                            f => f.ExternalId == program.Faculty.Id && f.IsActual),
                                    EducationLevel = await _dbContext.EducationLevels
                                        .FirstAsync(
                                            l => l.ExternalId == program.EducationLevel.Id && l.IsActual),
                                    IsActual = true
                                };
                                _dbContext.EducationPrograms.Add(newProgram);
                            }
                        }

                        await _dbContext.SaveChangesAsync();
                    }
                    break;
            }
        };
        return new ResponseModel
        {
            Message = "Directory was successfully updated",
            Status = "200"
        };
    }

    public async Task<List<EducationLevelModel>> GetEducationLevels()
    {
        var levels = await _dbContext.EducationLevels
            .Where(e => e.IsActual)
            .ToListAsync();
        var models = levels.Select(level => new EducationLevelModel
        {
            Id = level.ExternalId,
            Name = level.Name
        }).OrderBy(l => l.Id).ToList();
        return models;
    }

    public async Task<List<EducationDocumentTypeModel>> GetDocumentTypes()
    {
        var types = await _dbContext.DocumentTypes
            .Where(d => d.IsActual)
            .Include(d => d.EducationLevel)
            .Include(d => d.NextEducationLevels)
            .ToListAsync();
        var models = types.Select(type => new EducationDocumentTypeModel
        {
            CreateTime = type.CreateTime,
            EducationLevel = new EducationLevelModel
            {
                Id = type.EducationLevel.ExternalId,
                Name = type.EducationLevel.Name
            },
            Id = type.ExternalId,
            Name = type.Name,
            NextEducationLevels = new List<EducationLevelModel?>(type.NextEducationLevels.Select(level =>
                new EducationLevelModel
                {
                    Id = level.ExternalId,
                    Name = level.Name
                }))
        }).ToList();
        return models;
    }

    public async Task<List<FacultyModel>> GetFaculties()
    {
        var faculties = await _dbContext.Faculties
            .Where(f => f.IsActual)
            .ToListAsync();
        var models = faculties.Select(faculty => new FacultyModel
        {
            CreateTime = faculty.CreateTime,
            Id = faculty.ExternalId,
            Name = faculty.Name
        }).ToList();
        return models;
    }

    public async Task<ProgramPagedListModel> GetPrograms(
        int page, 
        int size,
        Guid? facultyId,
        int? educationLevelId,
        string? educationForm,
        string? language,
        string? nameOrCode)
    {
        var query = _dbContext.EducationPrograms
            .Where(p => p.IsActual)
            .Include(p => p.Faculty)
            .Include(p => p.EducationLevel)
            .AsQueryable();

        if (facultyId != null)
        {
            query = query.Where(p => p.Faculty.ExternalId == facultyId);
        }
        if (educationLevelId != null)
        {
            query = query.Where(p => p.EducationLevel.ExternalId == educationLevelId);
        }
        if (!string.IsNullOrWhiteSpace(educationForm))
        {
            query = query.Where(p => p.EducationForm.ToLower().Contains(educationForm.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(language))
        {
            query = query.Where(p => p.Language.ToLower().Contains(language.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(nameOrCode))
        {
            query = query.Where(p =>
                p.Name.ToLower().Contains(nameOrCode.ToLower()) ||
                (p.Code != null && p.Code.ToLower().Contains(nameOrCode.ToLower())));
        }

        var total = await query.CountAsync();

        var programs = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        var result = new ProgramPagedListModel
        {
            Programs = programs.Select(program => new EducationProgramModel
            {
                Id = program.ExternalId,
                Code = program.Code ?? "",
                CreateTime = program.CreateTime,
                EducationForm = program.EducationForm,
                EducationLevel = new EducationLevelModel
                {
                    Id = program.EducationLevel.ExternalId,
                    Name = program.EducationLevel.Name
                },
                Faculty = new FacultyModel
                {
                    CreateTime = program.Faculty.CreateTime,
                    Id = program.Faculty.ExternalId,
                    Name = program.Faculty.Name
                },
                Language = program.Language,
                Name = program.Name
            }).ToList(),
            Pagination = new PageInfoModel
            {
                Count = (int)Math.Ceiling((double)total / size),
                Current = page,
                Size = size
            }
        };

        return result;
    }
}