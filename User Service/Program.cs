using Microsoft.EntityFrameworkCore;
using User_Service.BusinessLogicLayer.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UsersDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(UsersDbContext)));
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();