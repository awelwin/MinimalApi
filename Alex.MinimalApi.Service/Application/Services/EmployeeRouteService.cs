using Alex.MinimalApi.Service.Core.Services;
using AutoMapper;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Application.Services
{
    /// <summary>
    /// Employee Application service
    /// </summary>
    public sealed class EmployeeRouteService
    {
        private IMapper mapper;
        private EmployeeService employeeService;

        public EmployeeRouteService(IMapper mapper, EmployeeService employeeService)
        {
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Handle Create Employee Route
        /// </summary>
        /// <param name="emp">employee</param>
        /// <returns>BadRequest or copy of new employee once created</returns>
        public async Task<IResult> PostAsync(Pres.Employee emp)
        {
            if (emp == null)
                return Results.BadRequest();

            Core.Employee input = mapper.Map<Core.Employee>(emp);
            var result = await employeeService.CreateAsync(input);
            Pres.Employee output = mapper.Map<Pres.Employee>(result);
            return Results.Created($"/Employee/{result.Id}", output);
        }
    }
}
