using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceSystem.WebApi.Controllers.BaseController;
using SpaceSystem.WebApi.Entity.Models.StarSystems;
using StarSystemWithEFCore.Data;
using DataStarSystem = StarSystemWithEFCore.Data.Entities.StarSystem;

namespace SpaceSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class StarSystemController : EntityControllerBase<StarSystemCreateModel, StarSystemReadModel,
    StarSystemUpdateModel, DataStarSystem>
{
    public StarSystemController(StarSystemContext context, IMapper mapper) : base(context, mapper)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IList<StarSystemReadModel>> Get(CancellationToken cancellationToken) =>
        await GetModels(cancellationToken);

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StarSystemReadModel>> Get(int id, CancellationToken cancellationToken)
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
    /// Creates a StarSystem.
    /// </summary>
    /// <param name="createModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A newly created StarSystem</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /StarSystem
    ///     {
    ///         "id": 0,
    ///         "name": "string",
    ///         "age": 0,
    ///         "centerOfGravityId": 0, // or null, if SpaceObject not created
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(StarSystemCreateModel createModel, CancellationToken cancellationToken)
    {
        await CreateModel(createModel, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = createModel.Id }, createModel);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StarSystemReadModel>> Put(int id, StarSystemUpdateModel updateModel,
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
    public async Task<ActionResult<StarSystemReadModel>> Delete(int id, CancellationToken cancellationToken)
    {
        var readModel = await DeleteModel(id, cancellationToken);

        if (readModel == null)
        {
            return NotFound();
        }

        return readModel;
    }
}