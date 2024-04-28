using Alex.MinimalApi.Service.Core;
using Alex.MinimalApi.Service.Repository.EntityFramework;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EF = Alex.MinimalApi.Service.Repository.EntityFramework;

namespace Alex.MinimalApi.Service.Repository
{
    /// <summary>
    /// Employee Repository
    /// </summary>
    public class EmployeeRepository : GenericRepository<Core.Employee, EF.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MinimalApiDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<Core.Employee> GetAsync(int id, bool expand = false)
        {

            if (expand)
            {

                //with children
                return await _context.Employees
                   .Where(e => e.Id == id)
                   .Include(c => c.TaxFile).ThenInclude(x => x!.TaxFileRecords)
                   .AsNoTracking()
                   .ProjectTo<Core.Employee>(_mapper.ConfigurationProvider)
                   .SingleOrDefaultAsync();
            }
            else
            {
                //without children
                return await base.GetAsync(id);
            }
        }

        public async Task<List<Core.Employee>> ListAsync(bool expand)
        {
            //dynamically create where condition
            Expression<Func<EF.Employee, bool>> criteria;
            //if (readyToCollect)
            //criteria = (a => (a.Submitted && a.Collection == null));

            criteria = (a => true);
            if (expand)
            {
                var myList = await _context.Employees
                    .Where(criteria)
                    .Include(c => c.TaxFile).ThenInclude(x => x!.TaxFileRecords)
                    .AsNoTracking()
                    .ProjectTo<Core.Employee>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return myList;
            }
            else
            {
                return await base.FindAsync(x => true);
            }
        }

    }
}
