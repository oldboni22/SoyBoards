#region

using Common.Data;
using SoyBoards.UserService.Domain.Contracts;
using SoyBoards.UserService.Domain.Entities;

#endregion

namespace SoyBoards.UserService.Infrastructure.Data;

public class UserServiceRepositoryManager(UserServiceDbContext context) : RepositoryManager(context), IUserServiceRepositoryManager
{
    public IRepository<User> UserRepository { get; } = new Repository<User>(context);

}
