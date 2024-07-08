namespace MyTech.DTOs;

public class ItemDTO
{
    public int ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemUrl { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}