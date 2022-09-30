using Microsoft.AspNetCore.Mvc;
using Parent.Application;

namespace ParentApi.Controllers;

[Route("api/guardians")]
[ApiController]
public class GuardianController : ControllerBase
{
    private readonly IGetAllGuardians _getAllGuardians;
    private readonly ICreateGuardian _createGuardian;
    private readonly IGetGuardianById _getGuardianById;

    public GuardianController(IGetAllGuardians getAllGuardians, ICreateGuardian createGuardian, IGetGuardianById getGuardianById)
    {
        _getAllGuardians = getAllGuardians;
        _createGuardian = createGuardian;
        _getGuardianById = getGuardianById;
    }

    [Route("{guardianId}")]
    [HttpGet]
    public async Task<IActionResult> GetByGuardianId(Guid guardianId)
    {
        var model = await _getGuardianById.HandleAsync(guardianId);

        return Ok(model);
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewParentViewModel newParentViewModel)
    {
        await _createGuardian.HandleAsync(newParentViewModel);

        return Ok();
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await _getAllGuardians.HandleAsync();

        return Ok(model);
    }
}