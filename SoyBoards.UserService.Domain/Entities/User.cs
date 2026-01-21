using Common.Data;

namespace SoyBoards.UserService.Domain.Entities;

public sealed class User : EntityBase
{
    public required string DisplayName { get; set; }
    
    public required string Tag { get; init; }

    public string? AvatarUrl { get; set; } = string.Empty;
}
