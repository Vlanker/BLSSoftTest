using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSystemWithEFCore.Data;
using StarSystemWithEFCore.Data.Entities;

namespace SpaceSystem.WebApi.Controllers.BaseController;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class EntityControllerBase<TCreateEntity, TReadEntity, TUpdateEntity, TData> : ControllerBase
    where TCreateEntity : class 
    where TReadEntity : class 
    where TUpdateEntity : class 
    where TData : class, IHaveIdentifier
{
    private readonly StarSystemContext _context;
    private readonly IMapper _mapper;

    protected EntityControllerBase(StarSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected virtual async Task<IList<TReadEntity>> GetModels(
        CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TData>()
            .AsNoTracking()
            .ProjectTo<TReadEntity>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    protected virtual async Task<TReadEntity?> GetModel(int id,
        CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TData>()
            .AsNoTracking()
            .Where(entity => entity.Id == id)
            .ProjectTo<TReadEntity>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    protected virtual async Task<TReadEntity?> CreateModel(TCreateEntity createModel,
        CancellationToken cancellationToken = default)
    {
        // create new entity from model
        var entity = _mapper.Map<TData>(createModel);

        // add to data set, id should be generated
        await _context
            .Set<TData>()
            .AddAsync(entity, cancellationToken);

        // save to database
        await _context
            .SaveChangesAsync(cancellationToken);

        // convert to read model
        var readModel = await GetModel(entity.Id, cancellationToken);

        return readModel;
    }

    protected virtual async Task<TReadEntity?> UpdateModel(int id, TUpdateEntity updateModel,
        CancellationToken cancellationToken = default)
    {
        var keyValues = new object[] { id };

        // find entity to update by id, not model id
        var entity = await _context 
            .Set<TData>()
            .FindAsync(keyValues, cancellationToken);

        if (entity == null)
        {
            return default;
        }

        // copy updates from model to entity
        _mapper.Map(updateModel, entity);

        // save updates
        await _context.SaveChangesAsync(cancellationToken);

        // return read model
        var readModel = await GetModel(id, cancellationToken);

        return readModel;
    }

    protected virtual async Task<TReadEntity?> DeleteModel(int id,
        CancellationToken cancellationToken = default)
    {
        var entities = _context.Set<TData>();
        var keyValues = new object[] { id };
        
        // find entity to delete by id
        var entity = await entities.FindAsync(keyValues, cancellationToken);

        if (entity == null)
        {
            return default;
        }

        // return read model
        var readModel = await GetModel(id, cancellationToken);

        // delete entry
        entities.Remove(entity);

        // save 
        await _context.SaveChangesAsync(cancellationToken);

        return readModel;
    }

    protected virtual async Task<IReadOnlyList<TReadEntity>> QueryModel(
        Expression<Func<TData, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        var entities = _context.Set<TData>();
        var query = entities.AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var results = await query
            .ProjectTo<TReadEntity>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return results;
    }
}