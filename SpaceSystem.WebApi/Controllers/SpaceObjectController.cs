using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceSystem.WebApi.Controllers.BaseController;
using SpaceSystem.WebApi.Entity.Models.SpaceObjects;
using StarSystemWithEFCore.Data;
using DataSpaceObject = StarSystemWithEFCore.Data.Entities.SpaceObject;

namespace SpaceSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SpaceObjectController : EntityControllerBase<SpaceObjectCreateModel, SpaceObjectReadModel,
    SpaceObjectUpdateModel, DataSpaceObject>
{
    public SpaceObjectController(StarSystemContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IList<SpaceObjectReadModel>> Get(CancellationToken cancellationToken) =>
        await GetModels(cancellationToken);

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpaceObjectReadModel>> Get(int id, CancellationToken cancellationToken)
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
    /// Creates a SpaceObject.
    /// </summary>
    /// <param name="createModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A newly created SpaceObject</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /SpaceObject
    ///     {
    ///         "id": 0,
    ///         "name": "Item #1",
    ///         "age": 0,
    ///         "diameter": 0,
    ///         "weight": 0,
    ///         "spaceObjectTypeId": 0,
    ///         "starSystemId": 0
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(SpaceObjectCreateModel createModel, CancellationToken cancellationToken)
    {
        await CreateModel(createModel, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = createModel.Id }, createModel);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpaceObjectReadModel>> Put(int id, SpaceObjectUpdateModel updateModel,
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
    public async Task<ActionResult<SpaceObjectReadModel>> Delete(int id, CancellationToken cancellationToken)
    {
        var readModel = await DeleteModel(id, cancellationToken);

        if (readModel == null)
        {
            return NotFound();
        }

        return readModel;
    }
}