using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Common;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        string xmlFilename)
    {
        services.AddSwaggerGen(x =>
        {
            x.SchemaFilter<EnumSchemaFilter>();
            
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "JWT Authentication",
                Description = "Please enter token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference()
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            
            x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            x.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
            
            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}