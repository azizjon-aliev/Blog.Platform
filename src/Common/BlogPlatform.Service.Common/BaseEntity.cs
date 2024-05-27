namespace BlogPlatform.Service.Common;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}