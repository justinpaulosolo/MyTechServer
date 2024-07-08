using Microsoft.EntityFrameworkCore;
using MyTech.Data;
using MyTech.Domain;
using MyTech.DTOs;

namespace MyTech.Services;

public interface ICollectionsService
{
    Task<IEnumerable<CollectionDTO>> GetCollectionAsync();
    Task<CollectionDTO?> GetCollectionByIdAsync(int id);
    Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto);
    Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto);
    Task<ItemDTO> AddItemToCollectionAsync(int itemId, int collectionId);
    Task DeleteCollectionAsync(int id);
}

public class CollectionsService(ApplicationDbContext context) : ICollectionsService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<IEnumerable<CollectionDTO>> GetCollectionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CollectionDTO?> GetCollectionByIdAsync(int id)
    {
        var collection = await _context.Collections.FindAsync(id);
        
        return collection?.ToCollectionDTO();
    }

    public async Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto)
    {
        var collection = new Collection
        {
            CollectionName = collectionDto.CollectionName,
            UserId = collectionDto.UserId
        };
        
        await _context.Collections.AddAsync(collection);
        await _context.SaveChangesAsync();
        
        return collection.ToCollectionDTO();
    }

    public async Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDTO> AddItemToCollectionAsync(int itemId, int collectionId)
    {
        var collection = await _context.Collections.Include(c => c.CollectionItems)
            .FirstOrDefaultAsync(c => c.CollectionId == collectionId);
        
        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
        
        if (collection == null)
            throw new InvalidOperationException("Collection not found");
        
        if (item == null)
            throw new InvalidOperationException("Item not found");
        
        var collectionItem = new CollectionItem
        {
            CollectionId = collectionId,
            ItemId = item.ItemId
        };
        
        collection.CollectionItems.Add(collectionItem);
        
        await _context.CollectionItems.AddAsync(collectionItem);
        await _context.SaveChangesAsync();
        
        return new ItemDTO
        {
            ItemId = item.ItemId,
            ItemName = item.ItemName,
            ItemDescription = item.ItemDescription,
            ItemUrl = item.ItemUrl,
        };
    }

    public async Task DeleteCollectionAsync(int id)
    {
        throw new NotImplementedException();
    }
}