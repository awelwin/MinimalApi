using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Repository.EntityFramework;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Alex.MinimalApi.Service.Repository
{
    public class GenericRepository<CoreType, EFType> : IRepository<CoreType>
        where CoreType : class, ICoreEntity
        where EFType : class, IRepositoryEntity
    {
        protected internal MinimalApiDbContext _context;
        protected internal IMapper _mapper;

        public GenericRepository(MinimalApiDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CoreType> CreateAsync(CoreType input)
        {
            //validate params
            if (input == null)
                throw new ArgumentNullException();

            //map to local entity framework
            EFType entity = _mapper.Map<EFType>(input);

            //persist changes
            _context.Set<EFType>().Add(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return _mapper.Map<CoreType>(entity);
        }

        public async Task<CoreType> GetAsync(int id)
        {
            return _mapper.Map<CoreType>(
                await _context.Set<EFType>().FindAsync(id));
        }

        public async Task<List<CoreType>> FindAsync(Expression<Func<CoreType, bool>> predicate)
        {

            var myList = await _context.Set<EFType>().ProjectTo<CoreType>(_mapper.ConfigurationProvider)
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();

            return myList;
        }

        public async Task<CoreType> UpdateAsync(CoreType details)
        {
            //validate params
            if (details == null)
                throw new ArgumentNullException();

            //update entity
            //EFType updated = _mapper.Map<EFType>(details);
            //_context.Set<EFType>().Update(updated);
            //await _context.SaveChangesAsync();
            //_context.Entry(updated).State = EntityState.Detached;
            //return _mapper.Map<CoreType>(updated);


            //check existing
            var existing = await _context.Set<EFType>().FindAsync(details.Id.GetValueOrDefault());
            if (existing == null)
                throw new ArgumentException($"entity with id: {details.Id} not found");

            //update
            EFType updated = _mapper.Map<EFType>(details);
            _context.Entry(existing).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();

            //stop tracking
            _context.Entry(existing).State = EntityState.Detached;

            //return current state
            return await GetAsync(existing.Id.GetValueOrDefault());

        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Set<EFType>().FindAsync(id);
            if (existing == null)
                throw new ArgumentException("existing entity not found");

            _context.Set<EFType>().Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
