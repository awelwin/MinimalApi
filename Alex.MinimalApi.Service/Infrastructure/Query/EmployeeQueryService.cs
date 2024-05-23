using Alex.MinimalApi.Service.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Alex.MinimalApi.Service.Infrastructure.Query
{
    public class EmployeeQueryService
    {
        protected internal MinimalApiDbContext _context;

        public EmployeeQueryService(MinimalApiDbContext context)
        {
            _context = context;

        }

        /// <summary>
        /// Find Employees where input is part of either first or lastname
        /// </summary>
        /// <param name="input">search criteria</param>
        /// <returns>List</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<List<EmployeeSearchQueryResult>> Search(string input)
        {
            throw new ArgumentException();
            if (String.IsNullOrEmpty(input))
                throw new ArgumentOutOfRangeException("input parameter missing");

            return (from emps in _context.Employees.Take(10)
                    where emps.Firstname.Contains(input) || emps.Lastname.Contains(input)
                    select new EmployeeSearchQueryResult()
                    {
                        Id = emps.Id,
                        Firstname = emps.Firstname,
                        Lastname = emps.Lastname
                    }).AsNoTracking()
                   .ToList<EmployeeSearchQueryResult>();
        }
    }
}
