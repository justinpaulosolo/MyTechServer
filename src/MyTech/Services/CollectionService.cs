using MyTech.Data;
using MyTech.DTOs;

namespace MyTech.Services;

public interface ICollectionService
{
    Task<IEnumerable<CollectionDTO>> GetCollectionAsync();
    Task<CollectionDTO> GetCollectionByIdAsync(int id);
    Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto);
    Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto);
    Task DeleteCollectionAsync(int id);
}

public class CollectionService(ApplicationDbContext context) : ICollectionService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<IEnumerable<CollectionDTO>> GetCollectionAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CollectionDTO> GetCollectionByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CollectionDTO> CreateCollectionAsync(CollectionDTO collectionDto)
    {
        throw new NotImplementedException();
    }

    public async Task<CollectionDTO> UpdateCollectionAsync(CollectionDTO collectionDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCollectionAsync(int id)
    {
        throw new NotImplementedException();
    }
}