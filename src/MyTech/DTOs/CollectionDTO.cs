namespace MyTech.DTOs;

public class CollectionDTO
{
    public int CollectionId { get; set; }
    public string CollectionName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public List<int> ItemsIds { get; set; }
}