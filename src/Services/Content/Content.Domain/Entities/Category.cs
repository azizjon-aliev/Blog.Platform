namespace Content.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
		
        public ICollection<Post> Posts { get; } = [];
    
        public DateTime CreatedAt { get; set; }
    
        public DateTime? UpdatedAt { get; set; }
    }
}