using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTech.Domain;
using MyTech.DTOs;
using MyTech.Services;

namespace MyTech.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController(ICollectionsService collectionsService, UserManager<User> userManager)
    : ControllerBase
{
    private readonly ICollectionsService _collectionsService = collectionsService;
    private readonly UserManager<User> _userManager = userManager;
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCollection(CreateCollectionDTO request)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var collection = new CollectionDTO
        {
            CollectionName = request.Name,
            UserId = user.Id
        };
        
        var createdCollection = await _collectionsService.CreateCollectionAsync(collection);
        
        return CreatedAtAction(nameof(GetCollection), new {id = createdCollection.CollectionId}, createdCollection);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCollection(int id)
    {
        var collection = await _collectionsService.GetCollectionByIdAsync(id);

        if (collection == null) return NotFound();

        return Ok(collection);
    }
}