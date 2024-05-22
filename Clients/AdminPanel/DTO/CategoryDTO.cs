namespace AdminPanel.DTO;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
}