

using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Alex.MinimalApi.Service.Configuration;
using Alex.MinimalApi.Service.Core.Services;
using AutoMapper;
using global::Alex.MinimalApi.Service.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Core = Alex.MinimalApi.Service.Core;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Test
{
    [TestClass]
    public class Test_EmployeeRouteHandler
    {
        IMapper? In_Mapper { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Test_EmployeeRouteHandler()
        {
            this.In_Mapper = AutoMapperConfig.ConfigureMaps();
        }

        [TestMethod()]
        public async Task GetEmployeeById_NotFound_Returns404()
        {
            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Employee>>();
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Employee>(null!));

            //ACT
            IResult actual = await EmployeeRouteHandler.GetEmployeeById(
                in_id,
                this.In_Mapper!,
                in_repo.Object);

            //ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFound<int>));
        }

        [TestMethod()]
        public async Task GetEmployeeById_Found_ReturnsEmployee()
        {

            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Employee>>();
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Employee>(
                    new Employee()
                    {
                        Id = in_id,
                        Firstname = "test",
                        Lastname = "test",
                        Age = 12
                    }));

            //ACT
            IResult actual = await EmployeeRouteHandler.GetEmployeeById(
                in_id,
                 this.In_Mapper!,
                 in_repo.Object);

            //ASSERT
            Assert.IsNotNull(actual); // returns result
            var okResult = (Ok<Pres.Employee>)actual;
            Assert.IsNotNull(okResult); //return 200 OK result
            Assert.IsNotNull(okResult.Value); //returns content
            Assert.IsInstanceOfType(okResult.Value, typeof(Pres.Employee)); //returns correct content type
        }

        [TestMethod()]
        public async Task UpdateEmployee_Success_ReturnsEmployee()
        {

            //ARANGE
            const int EXPECTED_ID = 3;
            const string EXPECTED_FIRSTNAME = "firstname";
            const string EXPECTED_LASTNAME = "lastname";
            const int EXPECTED_AGE = 23;

            Pres.Employee in_pres_emp = new Pres.Employee() { Id = EXPECTED_ID, Age = EXPECTED_AGE, Firstname = EXPECTED_FIRSTNAME, Lastname = EXPECTED_LASTNAME };
            Core.Employee in_core_emp = new Core.Employee() { Id = EXPECTED_ID, Age = EXPECTED_AGE, Firstname = EXPECTED_FIRSTNAME, Lastname = EXPECTED_LASTNAME };

            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.UpdateAsunc() dependency
            in_repo.Setup(x => x.UpdateAsync(It.IsAny<Core.Employee>()))
                    .Returns(Task.FromResult<Core.Employee>(in_core_emp));

            //ACT
            IResult actual = await EmployeeRouteHandler.UpdateEmployee(EXPECTED_ID,
                in_pres_emp,
                 this.In_Mapper!,
                 in_repo.Object);


            //ASSERT
            Assert.IsNotNull(actual); // returns result
            var okResult = (Ok<Pres.Employee>)actual;
            Assert.IsNotNull(okResult); //return 200 OK result
            Assert.IsNotNull(okResult.Value); //returns content
            Assert.IsInstanceOfType(okResult.Value, typeof(Pres.Employee)); //returns correct content type
            Assert.AreEqual(EXPECTED_ID, ((Pres.Employee)okResult.Value).Id);
        }

        [TestMethod()]
        public async Task UpdateEmployee_badRequest_ReturnsBadRequest()
        {

            //ARRANGE
            var in_repo = new Mock<IRepository<Core.Employee>>();

            //ACT
            IResult actual = await EmployeeRouteHandler.UpdateEmployee(null,
               null!,
                this.In_Mapper!,
                in_repo.Object);

            //ASSERT
            Assert.IsNotNull(actual);
            var badRequestResult = (BadRequest)actual;
            Assert.IsNotNull(badRequestResult); //correct result type
            Assert.IsInstanceOfType(badRequestResult, typeof(BadRequest));
        }

    }
}
