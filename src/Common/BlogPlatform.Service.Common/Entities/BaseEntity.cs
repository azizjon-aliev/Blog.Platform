namespace BlogPlatform.Service.Common.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}