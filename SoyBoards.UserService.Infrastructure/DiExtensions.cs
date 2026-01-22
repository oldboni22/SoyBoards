#region

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoyBoards.UserService.Domain.Contracts;
using SoyBoards.UserService.Infrastructure.Constants;
using SoyBoards.UserService.Infrastructure.Data;

#endregion

namespace SoyBoards.UserService.Infrastructure;

public static class DiExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection RegisterInfrastructure(IConfiguration configuration)
        {
            return services
                .AddDbContext(configuration);
        }

        private IServiceCollection AddDbContext(IConfiguration configuration)
        {
            var connectionString = configuration[ConfigurationKeys.DbConnectionString];

            return services.AddDbContext<UserServiceDbContext>(builder =>
            {
                builder.UseNpgsql(connectionString);
            });
        }

        private IServiceCollection AddRepositories()
        {
            return services
                .AddScoped<IUserServiceRepositoryManager, UserServiceRepositoryManager>();
        }
    }
}
