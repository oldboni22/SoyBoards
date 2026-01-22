#region

using Common.Data;
using SoyBoards.UserService.Domain.Entities;

#endregion

namespace SoyBoards.UserService.Domain.Contracts;

public interface IUserServiceRepositoryManager : IRepositoryManager
{
    IRepository<User> UserRepository { get;}
}
