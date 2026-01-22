using Common.Blob;
using Common.Blob.Azure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAzureBlobService(IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration[AzureBlobConstants.StorageConnectionStringKey]);
            });
            
            return services
                .Configure<AzureBlobServiceOptions>(configuration.GetSection(AzureBlobConstants.SectionName))
                .AddScoped<IBlobService, AzureBlobService>();
        }
    }
}
