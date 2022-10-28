using Microsoft.AspNetCore.Mvc;
using Parent.Application;
using Parent.Application.MessageModels;

namespace ParentApi.Controllers;

[Route("api/toys")]
[ApiController]
public class ToyController : ControllerBase
{
    private readonly IToyNameUpdated _toyNameUpdated;

    public ToyController(IToyNameUpdated toyNameUpdated)
    {
        _toyNameUpdated = toyNameUpdated;
    }
    
    [Route("{upc}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateName(string upc, [FromBody] ToyNameChangedViewModel viewModel)
    {
        await _toyNameUpdated.HandleAsync(viewModel);

        return Ok();
    }
}