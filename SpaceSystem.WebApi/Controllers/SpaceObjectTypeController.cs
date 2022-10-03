using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceSystem.WebApi.Controllers.BaseController;
using SpaceSystem.WebApi.Entity.Models.SpaceObjectTypes;
using StarSystemWithEFCore.Data;
using DataSpaceObjectType = StarSystemWithEFCore.Data.Entities.SpaceObjectType;

namespace SpaceSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SpaceObjectTypeController : EntityControllerBase<SpaceObjectTypeCreateModel, SpaceObjectTypeReadModel,
    SpaceObjectTypeUpdateModel, DataSpaceObjectType>
{
    public SpaceObjectTypeController(StarSystemContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IList<SpaceObjectTypeReadModel>> Get(CancellationToken cancellationToken) =>
        await GetModels(cancellationToken);

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpaceObjectTypeReadModel>> Get(int id, CancellationToken cancellationToken)
    {
        var item = await GetModel(id, cancellationToken);

        if (item is null)
        {
            return NotFound();
        }

        return item;
    }

    // <snippet_Create>
    /// <summary>
    /// Creates a SpaceObjectType.
    /// </summary>
    /// <param name="createModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A newly created SpaceObjectType</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /SpaceObjectType
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(SpaceObjectTypeCreateModel createModel, CancellationToken cancellationToken)
    {
        await CreateModel(createModel, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = createModel.Id }, createModel);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpaceObjectTypeReadModel>> Put(int id, SpaceObjectTypeUpdateModel updateModel,
        CancellationToken cancellationToken)
    {
        var readModel = await UpdateModel(id, updateModel, cancellationToken);

        if (readModel == null)
        {
            return NotFound();
        }

        return readModel;
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpaceObjectTypeReadModel>> Delete(int id, CancellationToken cancellationToken)
    {
        var readModel = await DeleteModel(id, cancellationToken);

        if (readModel == null)
        {
            return NotFound();
        }

        return readModel;
    }
}