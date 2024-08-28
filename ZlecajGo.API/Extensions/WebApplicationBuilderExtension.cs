using Microsoft.OpenApi.Models;
using Serilog;
using ZlecajGo.API.Middlewares;

namespace ZlecajGo.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    []
                }
            });
        });
        
        builder.Host.UseSerilog((context, logger) =>
        {
            logger.ReadFrom.Configuration(context.Configuration);
        });
        
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}