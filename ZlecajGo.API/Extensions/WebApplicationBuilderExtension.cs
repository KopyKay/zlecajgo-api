using Serilog;

namespace ZlecajGo.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Host.UseSerilog((context, logger) =>
        {
            logger.ReadFrom.Configuration(context.Configuration);
        });
    }
}