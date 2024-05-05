using Alex.MinimalApi.Service.Application.Services;
using Alex.MinimalApi.Service.Configuration;
using Alex.MinimalApi.Service.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Core = Alex.MinimalApi.Service.Core;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Test
{
    [TestClass]
    public class Test_GenericRouteService
    {
        IMapper? In_Mapper { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Test_GenericRouteService()
        {
            this.In_Mapper = AutoMapperConfig.ConfigureMaps();
        }

        [TestMethod()]
        public async Task PostEntityAsync_Success_ReturnsEntity()
        {

            //ARANGE
            const int EXPECTED_ID = 3;
            const string EXPECTED_FIRSTNAME = "firstname";
            const string EXPECTED_LASTNAME = "lastname";
            const int EXPECTED_AGE = 23;
            const string EXPECTED_CREATED_URI = $"/Employee/3";

            Pres.Employee in_pres_emp = new Pres.Employee() { Age = EXPECTED_AGE, Firstname = EXPECTED_FIRSTNAME, Lastname = EXPECTED_LASTNAME };
            Core.Employee core_emp = new Core.Employee() { Age = EXPECTED_AGE, Firstname = EXPECTED_FIRSTNAME, Lastname = EXPECTED_LASTNAME };

            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.CreateAsunc() dependency
            in_repo.Setup(x => x.CreateAsync(It.IsAny<Core.Employee>()))
                    .Returns(Task.FromResult<Core.Employee>(
                        new Core.Employee()
                        {
                            Id = EXPECTED_ID,
                            Firstname = EXPECTED_FIRSTNAME,
                            Lastname = EXPECTED_LASTNAME,
                            Age = EXPECTED_AGE
                        }));
            EntityService<Core.Employee> in_employeeService = new EntityService<Core.Employee>(in_repo.Object);

            RouteService<Pres.Employee, Core.Employee> employeeRouteService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_employeeService);

            //ACT
            IResult actual = await employeeRouteService.PostAsync(in_pres_emp);

            //ASSERT
            Assert.IsNotNull(actual); // returns result
            var createdResult = (Created<Pres.Employee>)actual;
            Assert.IsNotNull(createdResult); //return 200 OK result
            Assert.IsNotNull(createdResult.Value); //returns content
            Assert.IsInstanceOfType(createdResult.Value, typeof(Pres.Employee)); //returns correct content type
            Assert.AreEqual(EXPECTED_ID, ((Pres.Employee)createdResult.Value).Id);
            Assert.IsFalse(String.IsNullOrEmpty(createdResult.Location)); //return uri of newly created 
            Assert.AreEqual(EXPECTED_CREATED_URI, (createdResult.Location)); //return correct uri of newly created
        }

        [TestMethod()]
        public async Task PostEntityAsync_WithNoBody_ReturnsBadRequest()
        {

            //ARRANGE
            var in_repo = new Mock<IRepository<Core.Employee>>();
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> employeeRouteService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);

            //ACT
            IResult actual = await employeeRouteService.PostAsync(null!);

            //ASSERT
            Assert.IsNotNull(actual);
            var badRequestResult = (BadRequest)actual;
            Assert.IsNotNull(badRequestResult); //correct result type
            Assert.IsInstanceOfType(badRequestResult, typeof(BadRequest));
        }
    }
}
