using System.Text.Json;
using Common.DtoModels.Directory;
using Common.Interfaces.DirectoryService;
using Microsoft.AspNetCore.WebUtilities;

namespace Directory_Service.BusinessLogicLayer.Services;

public class ExternalApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApplicantDictionaryApiClient");
    }

    public async Task<List<EducationLevelModel>> ImportEducationLevelsAsync()
    {
        var response = await _httpClient.GetAsync("education_levels");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<EducationLevelModel>>(
            json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result ?? [];
    }

    public async Task<List<EducationDocumentTypeModel>> ImportDocumentTypesAsync()
    {
        var response = await _httpClient.GetAsync("document_types");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<EducationDocumentTypeModel>>(
            json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result ?? [];
    }

    public async Task<List<FacultyModel>> ImportFacultiesAsync()
    {
        var response = await _httpClient.GetAsync("faculties");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<FacultyModel>>(
            json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result ?? [];
    }

    public async Task<PageInfoModel> GetProgramsPageInfoModel(int size)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["page"] = "1",
            ["size"] = size.ToString()
        };
        var url = QueryHelpers.AddQueryString("programs", queryParams);
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProgramPagedListModel>(
            json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result.Pagination;
    }

    public async Task<List<EducationProgramModel>> ImportProgramsAsync(int page, int size)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["page"] = page.ToString(),
            ["size"] = size.ToString()
        };
        var url = QueryHelpers.AddQueryString("programs", queryParams);
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProgramPagedListModel>(
            json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result?.Programs ?? [];
    }
}