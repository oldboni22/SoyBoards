using Common.Data;
using Microsoft.EntityFrameworkCore;
using SoyBoards.UserService.Domain.Contracts;
using SoyBoards.UserService.Domain.Entities;

namespace SoyBoards.UserService.Infrastructure.Data.Repositories;

public class UserRepository(UserServiceDbContext context) : Repository<User>(context), IUserRepository
{
    public Task<bool> TagExistsAsync(string tag)
    {
        return context.Users.AnyAsync(u => EF.Functions.ILike(u.Tag, tag));
    }
}
