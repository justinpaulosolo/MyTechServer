using MyTech.DTOs;

namespace MyTech.Domain;

public class Collection
{
    public int CollectionId { get; set; }
    public string CollectionName { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    
    // Self-referencing relationship for sub-collections
    public int? ParentCollectionId { get; set; }
    
    // Foreign Key for the user
    public string UserId { get; set; } = string.Empty;
    
    // Navigation Property
    public User User { get; set; } = null!;
    public Collection ParentCollection { get; set; }
    public List<Collection> SubCollection { get; set; } = [];
}