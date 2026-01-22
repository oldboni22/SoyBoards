using System.ComponentModel.DataAnnotations;
using Common.Data;
using SoyBoards.UserService.Domain.Constants;

namespace SoyBoards.UserService.Domain.Entities;

public sealed class User : EntityBase
{
    [MaxLength(UserEntityConstants.MaxNameLength)]
    public required string DisplayName { get; set; }
    
    [MaxLength(UserEntityConstants.MaxTagLength)]
    public required string Tag { get; init; }
    
    public string? AvatarUrl { get; set; }
}
