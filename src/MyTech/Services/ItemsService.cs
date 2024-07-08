using MyTech.Data;
using MyTech.Domain;
using MyTech.DTOs;

namespace MyTech.Services;

public interface IItemsService
{
    Task<IEnumerable<ItemDTO>> GetCollectionAsync();
    Task<ItemDTO?> GetItemByIdAsync(int id);
    Task<ItemDTO> CreateItemAsync(ItemDTO itemDto);
    Task<ItemDTO> UpdateCollectionAsync(ItemDTO itemDto);
    Task DeleteCollectionAsync(int id);
}

public class ItemsService : IItemsService
{
    private readonly ApplicationDbContext _context;

    public ItemsService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ItemDTO>> GetCollectionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDTO?> GetItemByIdAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        
        return item?.ToItemDTO();
    }

    public async Task<ItemDTO> CreateItemAsync(ItemDTO itemDto)
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
        
        return item.ToItemDTO();
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