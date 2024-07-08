using MyTech.DTOs;

namespace MyTech.Domain;

public class Item
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemUrl { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    // Foreign Key for the collection
    public string UserId { get; set; } = string.Empty;
    public int CollectionId { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;
    public Collection Collection { get; set; } = null!;
}