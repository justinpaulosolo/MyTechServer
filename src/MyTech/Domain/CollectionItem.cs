namespace MyTech.Domain;

public class CollectionItem
{
    public int CollectionId { get; set; }
    public int ItemId { get; set; }
    
    public Collection Collection { get; set; } = null!;
    public Item Item { get; set; } = null!;
}