using Common.Data;
using SoyBoards.UserService.Domain.Contracts;
using SoyBoards.UserService.Infrastructure.Data.Repositories;

namespace SoyBoards.UserService.Infrastructure.Data;

public class UserServiceRepositoryManager(UserServiceDbContext context) : RepositoryManager(context), IUserServiceRepositoryManager
{
    public IUserRepository UserRepository { get; } = new UserRepository(context);
}
