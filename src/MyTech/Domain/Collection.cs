namespace MyTech.Domain;

public class Collection
{
    public int CollectionId { get; set; }
    public string CollectionName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Key for the user
    public string UserId { get; set; } = string.Empty;
    
    // Navigation Property
    public User User { get; set; } = null!;
    public ICollection<CollectionItem> CollectionItems { get; set; } = new List<CollectionItem>();
}