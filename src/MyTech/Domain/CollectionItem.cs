namespace MyTech.Domain;

public class CollectionItem
{
    public int CollectionId { get; set; }
    public Collection Collection { get; set; } = null!;
    
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
}