using Common.Data;
using SoyBoards.UserService.Domain.Entities;

namespace SoyBoards.UserService.Domain.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task<bool> TagExistsAsync(string tag);
}
