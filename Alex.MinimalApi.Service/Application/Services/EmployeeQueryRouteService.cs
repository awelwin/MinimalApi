using Alex.MinimalApi.Service.Core.Services;
using Alex.MinimalApi.Service.Infrastructure.Query;
using AutoMapper;

namespace Alex.MinimalApi.Service.Application.Services
{
    public class EmployeeQueryRouteService
    {
        private IRepository<Core.Employee> _repo;
        private Infrastructure.Query.EmployeeQueryService _query;


        public EmployeeQueryRouteService(
           IRepository<Core.Employee> repo,
            EmployeeQueryService query,
            IMapper mapper)
        { this._repo = repo; this._query = query; }


        /// <summary>
        /// Find Employees where input is part of either first or lastname
        /// </summary>
        /// <param name="input">search criteria</param>
        /// <returns>List</returns>
        public async Task<List<EmployeeSearchQueryResult>> EmployeeSearch(string input)
        {
            //validate params
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("input parameter must be specified");


            List<EmployeeSearchQueryResult> returnObject = new List<EmployeeSearchQueryResult>();

            //Search ID
            int numberDetected = -1;
            if (Int32.TryParse(input, out numberDetected))
            {
                var result = await _repo.GetAsync(numberDetected);
                if (result != null)
                    returnObject.Add(new EmployeeSearchQueryResult()
                    {
                        Id = result.Id!.Value,
                        Firstname = result.Firstname,
                        Lastname = result.Lastname
                    });
                return returnObject;

            }

            //Search First / Lastname
            return await _query.Search(input);
        }
    }
}
