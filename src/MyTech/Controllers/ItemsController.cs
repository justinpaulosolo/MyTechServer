using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTech.Domain;
using MyTech.DTOs;
using MyTech.Services;

namespace MyTech.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemsService _itemsService;
    private readonly ICollectionsService _collectionsService;
    private readonly UserManager<User>_userManager;

    public ItemsController(IItemsService itemsService, ICollectionsService collectionsService, UserManager<User> userManager)
    {
        _itemsService = itemsService;
        _collectionsService = collectionsService;
        _userManager = userManager;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateItem(CreateItemDTO request)
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
        {
            return Unauthorized();
        }
        
        var item = new ItemDTO()
        {
            ItemName = request.ItemName,
            ItemUrl = request.ItemUrl,
            ItemDescription = request.ItemDescription,
            UserId = user.Id
        };
        
        var createdItem = await _itemsService.CreateItemAsync(item);
        
        return CreatedAtAction(nameof(GetItem), new {id = createdItem}, createdItem);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetItem(int id)
    {
        var item = await _itemsService.GetItemByIdAsync(id);

        if (item == null) return NotFound();

        return Ok(item);
    }
}