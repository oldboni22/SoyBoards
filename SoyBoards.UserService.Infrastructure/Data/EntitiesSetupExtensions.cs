using Microsoft.EntityFrameworkCore;
using SoyBoards.UserService.Domain.Entities;

namespace SoyBoards.UserService.Infrastructure.Data;

public static class EntitiesSetupExtensions
{
    extension(ModelBuilder modelBuilder)
    {
        public ModelBuilder SetUpUser()
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Tag)
                .IsUnique(); //enforce unique tag 

            return modelBuilder;
        }
    }
}