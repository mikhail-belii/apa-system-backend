using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Common;
using Common.Middlewares;
using Directory_Service.BusinessLogicLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DirectoryDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(DirectoryDbContext)));
});
builder.Services.AddBusinessLogic();

var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
builder.Services.AddSwaggerConfiguration(xmlFilename);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddControllers();

builder.Services.AddHttpClient("ApplicantDictionaryApiClient", client =>
{
    var baseAddress = configuration.GetSection("ApplicantDictionaryApiClient:BaseAddress")
        .Get<string>();
    var login = configuration.GetSection("ApplicantDictionaryApiClient:Login")
        .Get<string>();
    var password = configuration.GetSection("ApplicantDictionaryApiClient:Password")
        .Get<string>();
    client.BaseAddress = new Uri(baseAddress);
    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Basic", credentials);
});

var app = builder.Build();

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();