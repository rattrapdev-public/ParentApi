using Microsoft.AspNetCore.Mvc;
using Parent.Application;
using Parent.Application.MessageModels;
using Parent.Application.ViewModels;

namespace ParentApi.Controllers;

[Route("api/children")]
public class ChildController : ControllerBase
{
    private readonly IAllChildren _allChildren;
    private readonly ICreateChild _createChild;
    private readonly IToyPurchased _toyPurchased;

    public ChildController(IAllChildren allChildren, ICreateChild createChild, IToyPurchased toyPurchased)
    {
        _allChildren = allChildren;
        _createChild = createChild;
        _toyPurchased = toyPurchased;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewChildViewModel newChildViewModel)
    {
        await _createChild.HandleAsync(newChildViewModel);

        return Ok();
    }

    [Route("{childId}/toy")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PurchasedToyViewModel purchasedToyViewModel)
    {
        await _toyPurchased.HandleAsync(purchasedToyViewModel);

        return Ok();
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await _allChildren.All();

        return Ok(model);
    }
}