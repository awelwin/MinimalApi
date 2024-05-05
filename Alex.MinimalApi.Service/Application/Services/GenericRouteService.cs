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
    public class GenericRouteService<P, C>
        where P : PresentationEntity
        where C : CoreEntity
    {
        private IMapper mapper;
        private GenericEntityService<C> entityService;

        public GenericRouteService(IMapper mapper, GenericEntityService<C> entityService)
        {
            this.mapper = mapper;
            this.entityService = entityService;
        }

        /// <summary>
        /// Handle create entity Route
        /// </summary>
        /// <param name="entity">entity to persist</param>
        /// <returns>BadRequest or copy of new entity once created</returns>
        public async Task<IResult> PostEntityAsync(P entity)
        {
            if (entity == null)
                return Results.BadRequest();

            C input = mapper.Map<C>(entity);
            var result = await entityService.CreateAsync(input);
            P output = mapper.Map<P>(result);
            string uri = $"/{entity.GetType().Name}/{result.Id}";
            return Results.Created(uri, output);
        }
    }
}
