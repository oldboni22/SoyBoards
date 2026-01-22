using Common.Data;
using Microsoft.EntityFrameworkCore;
using SoyBoards.UserService.Domain.Entities;

namespace SoyBoards.UserService.Infrastructure.Data;

public class UserServiceDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .SetUpUser();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddTimeStampInterceptor();
    }
}
