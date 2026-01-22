using Common.Data;

namespace SoyBoards.UserService.Domain.Contracts;

public interface IUserServiceRepositoryManager : IRepositoryManager
{
    IUserRepository UserRepository { get;}
}
