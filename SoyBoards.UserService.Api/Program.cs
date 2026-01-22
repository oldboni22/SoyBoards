

using SoyBoards.UserService.Infrastructure;

namespace SoyBoards.UserService.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); 
        
        builder.Services.AddOpenApi();

        builder.Services.RegisterInfrastructure(builder.Configuration);
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.Run();
    }
}
