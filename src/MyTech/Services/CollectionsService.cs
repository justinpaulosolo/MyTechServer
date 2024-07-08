using Microsoft.EntityFrameworkCore;
using MyTech.Data;
using MyTech.Domain;
using MyTech.DTOs;

namespace MyTech.Services;

public interface ICollectionsService
{
    Task<IEnumerable<CollectionDTO>> GetCollectionAsync();
    Task<CollectionDTO?> GetCollectionByIdAsync(int id);
    Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto, string userId);
    Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto);
    Task<ItemDTO> AddItemToCollectionAsync(int itemId, int collectionId);
    Task DeleteCollectionAsync(int id);
}

public class CollectionsService : ICollectionsService
{
    private readonly ApplicationDbContext _context;
    
    public CollectionsService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CollectionDTO>> GetCollectionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CollectionDTO?> GetCollectionByIdAsync(int id)
    {
        var collection = await _context.Collections
            .Include(c => c.ParentCollection)
            .Include(c => c.SubCollection)
            .Include(c => c.CollectionItems)
            .ThenInclude(ci => ci.Item)
            .FirstOrDefaultAsync(c => c.CollectionId == id);
        
        if (collection == null)
            return null;
        
        return new CollectionDTO
        {
            CollectionId = collection.CollectionId,
            CollectionName = collection.CollectionName,
            CreatedAt = collection.CreatedAt,
            ModifiedAt = collection.ModifiedAt,
            ItemsIds = collection.CollectionItems.Select(i => i.ItemId).ToList()
        };
    }

    public async Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto, string userId)
    {
        var collection = new Collection
        {
            CollectionName = collectionDto.CollectionName,
            UserId = userId
        };
        
        await _context.Collections.AddAsync(collection);
        await _context.SaveChangesAsync();
        
        return new CollectionDTO
        {
            CollectionId = collection.CollectionId,
            CollectionName = collection.CollectionName,
            CreatedAt = collection.CreatedAt,
            ModifiedAt = collection.ModifiedAt,
        };
    }

    public async Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ItemDTO> AddItemToCollectionAsync(int itemId, int collectionId)
    {
        var collection = await _context.Collections
            .Include(c => c.CollectionItems)
            .FirstOrDefaultAsync(c => c.CollectionId == collectionId);
        
        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
        
        if (collection == null)
            throw new InvalidOperationException("Collection not found");
        
        if (item == null)
            throw new InvalidOperationException("Item not found");
        
        if (collection.CollectionItems.Any(i => i.ItemId == item.ItemId))
            throw new InvalidOperationException("Item already exists in collection");
        
        var collectionItem = new CollectionItem
        {
            CollectionId = collection.CollectionId,
            ItemId = item.ItemId
        };
        
        await _context.CollectionItems.AddAsync(collectionItem);
        await _context.SaveChangesAsync();
        
        collection.CollectionItems.Add(collectionItem);
        
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