using Microsoft.AspNetCore.Mvc;
using Parent.Application;
using Parent.Application.ViewModels;

namespace ParentApi.Controllers;

[Route("api/children")]
public class ChildController : ControllerBase
{
    private readonly IAllChildren _allChildren;
    private readonly ICreateChild _createChild;

    public ChildController(IAllChildren allChildren, ICreateChild createChild)
    {
        _allChildren = allChildren;
        _createChild = createChild;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewChildViewModel newChildViewModel)
    {
        await _createChild.HandleAsync(newChildViewModel);

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