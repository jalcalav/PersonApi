using System.ComponentModel.DataAnnotations;

namespace PersonApi.Domain;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    [Key] public Guid Id { get; }
}
