using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Data;

public class EntityBase
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
