using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Core.Services;
using Alex.MinimalApi.Service.Presentation;
using AutoMapper;

namespace Alex.MinimalApi.Service.Application.Services
{
    /// <summary>
    /// handle route logic for given entity type
    /// </summary>
    /// <typeparam name="P">Presentation entity type being routed</typeparam>
    /// <typeparam name="C">Core entity type being routed</typeparam>
    public class RouteService<P, C>
        where P : PresentationEntity
        where C : CoreEntity
    {
        private IMapper mapper;
        private EntityService<C> entityService;

        public RouteService(IMapper mapper, EntityService<C> entityService)
        {
            this.mapper = mapper;
            this.entityService = entityService;
        }

        /// <summary>
        /// Handle create entity Route
        /// </summary>
        /// <param name="entity">entity to persist</param>
        /// <returns>BadRequest or copy of new entity once created</returns>
        public async Task<IResult> PostAsync(P entity)
        {
            if (entity == null)
                return Results.BadRequest();

            C input = mapper.Map<C>(entity);
            var result = await entityService.CreateAsync(input);
            P output = mapper.Map<P>(result);
            string uri = $"/{entity.GetType().Name}/{result.Id}";
            return Results.Created(uri, output);
        }

        /// <summary>
        /// Handle update entity Route
        /// </summary>
        /// <param name="entity">entity to update</param>
        /// <returns>BadRequest or copy of new entity once updated</returns>
        public async Task<IResult> PutAsync(int id, P entity)
        {
            if (entity == null || id < 1)
                return Results.BadRequest();

            C input = mapper.Map<C>(entity);
            var result = await entityService.UpdateAsync(input);
            P output = mapper.Map<P>(result);
            string uri = $"/{entity.GetType().Name}";
            return Results.Ok<P>(output);
        }

        /// <summary>
        /// Handle Get entities Route
        /// </summary>
        /// <returns>All entities</returns>
        public async Task<IResult> GetAsync()
        {
            List<C> result = await entityService.GetAsync();
            List<P> output = mapper.Map<List<P>>(result);
            return Results.Ok(output);
        }

        /// <summary>
        /// Handle Get by id entities Route
        /// </summary>
        /// <returns>Entity represented by unique id</returns>
        public async Task<IResult> GetAsync(int id)
        {
            C result = await entityService.GetAsync(id);
            P output = mapper.Map<P>(result);
            if (output == null)
            {
                return Results.NotFound(id);
            }
            else
            {

                return Results.Ok(output);
            }
        }
    }
}
