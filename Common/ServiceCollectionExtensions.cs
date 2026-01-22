using Common.Blob;
using Common.Blob.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAzureBlobService(IConfiguration configuration)
        {
            return services
                .Configure<AzureBlobServiceOptions>(configuration.GetSection(AzureBlobServiceOptions.SectionName))
                .AddScoped<IBlobService, AzureBlobService>();
        }
    }
}