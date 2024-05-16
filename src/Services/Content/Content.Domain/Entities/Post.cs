namespace Content.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = null!;

        public string? Content { get; set; } = String.Empty;

        public Guid CategoryId { get; set; }

        public Category Category { get; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}