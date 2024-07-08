using MyTech.Data;
using MyTech.Domain;
using MyTech.DTOs;

namespace MyTech.Services;

public interface IItemsService
{
    Task<IEnumerable<ItemDTO>> GetCollectionAsync();
    Task<ItemDTO?> GetItemByIdAsync(int id);
    Task<ItemDTO> CreateItemAsync(ItemDTO itemDto, int collectionId);
    Task<ItemDTO> UpdateCollectionAsync(ItemDTO itemDto);
    Task DeleteCollectionAsync(int id);
}

public class ItemsService : IItemsService
{
    private readonly ApplicationDbContext _context;
    private readonly ICollectionsService _collectionsService;

    public ItemsService(ApplicationDbContext context, ICollectionsService collectionsService)
    {
        _context = context;
        _collectionsService = collectionsService;
    }
    
    public async Task<IEnumerable<ItemDTO>> GetCollectionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDTO?> GetItemByIdAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        
        if (item == null)
            return null;
        
        return new ItemDTO
        {
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            ItemDescription = item.ItemDescription,
            ItemUrl = item.ItemUrl,
            CollectionId = item.CollectionId,
            CreatedAt = item.CreatedAt,
            ModifiedAt = item.ModifiedAt,
        };
    }

    public async Task<ItemDTO> CreateItemAsync(ItemDTO itemDto, int collectionId)
    {
        var item = new Item
        {
            ItemName = itemDto.ItemName,
            ItemUrl = itemDto.ItemUrl,
            ItemDescription = itemDto.ItemDescription,
            UserId = itemDto.UserId
        };
        
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        
        await _collectionsService.AddItemToCollectionAsync(item.ItemId, collectionId);
        
        return new ItemDTO
        {
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            ItemDescription = item.ItemDescription,
            ItemUrl = item.ItemUrl,
            CollectionId = item.CollectionId,
            CreatedAt = item.CreatedAt,
            ModifiedAt = item.ModifiedAt,
        };
    }

    public async Task<ItemDTO> UpdateCollectionAsync(ItemDTO itemDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCollectionAsync(int id)
    {
        throw new NotImplementedException();
    }
}