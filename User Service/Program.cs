using System.Reflection;
using System.Text.Json.Serialization;
using Common;
using Common.Middlewares;
using Microsoft.EntityFrameworkCore;
using User_Service.BusinessLogicLayer.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(UsersDbContext)));
});
builder.Services.AddBusinessLogic();
builder.Services.AddScoped<TokenValidationFilter>();

var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
builder.Services.AddSwaggerConfiguration(xmlFilename);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<TokenValidationFilter>();
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