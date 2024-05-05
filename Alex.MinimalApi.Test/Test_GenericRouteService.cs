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
        public async Task PostAsync_Success_ReturnsEntity()
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

            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_employeeService);

            //ACT
            IResult actual = await routeService.PostAsync(in_pres_emp);

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
        public async Task PostAsync_WithNoBody_ReturnsBadRequest()
        {

            //ARRANGE
            var in_repo = new Mock<IRepository<Core.Employee>>();
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);

            //ACT
            IResult actual = await routeService.PostAsync(null!);

            //ASSERT
            Assert.IsNotNull(actual);
            var badRequestResult = (BadRequest)actual;
            Assert.IsNotNull(badRequestResult); //correct result type
            Assert.IsInstanceOfType(badRequestResult, typeof(BadRequest));
        }

        [TestMethod]
        public async Task GetAsync_MultipleExists_ReturnsList()
        {

            //ARRANGE
            int EXPECTED_COUNT = 3;
            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.CreateAsunc() dependency
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);

            in_repo.Setup(x => x.FindAsync(x => true))
                    .Returns(Task.FromResult<List<Core.Employee>>(
                        new List<Core.Employee>()
                        {
                            new Core.Employee() { Firstname = "test", Lastname = "test", Age = 23 },
                            new Core.Employee() { Firstname = "test", Lastname = "test", Age = 23 },
                            new Core.Employee() { Firstname = "test", Lastname = "test", Age = 23 }
                        }
                        ));
            //ACT

            IResult actual = await routeService.GetAsync();

            //ASSERT
            Assert.IsNotNull(actual);
            var okResult = (Ok<List<Pres.Employee>>)actual;
            Assert.IsNotNull(okResult); // returned without error
            Assert.IsInstanceOfType(okResult, typeof(Ok<List<Pres.Employee>>)); //OK 200 returned
            Assert.IsNotNull(okResult.Value); //content returned
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Pres.Employee>)); //list returned
            Assert.AreEqual(okResult.Value.Count, EXPECTED_COUNT); //3 employees returned

        }

        [TestMethod]
        public async Task GetAsync_EmployeesExist_ReturnsEmployees()
        {

            //ARRANGE
            int EXPECTED_EMPLOYEE_COUNT = 1;
            int EXPECTED_TAXFILERECORDS_COUNT = 2;

            Core.Employee emp = new Core.Employee()
            {
                Id = 1,
                Firstname = "first",
                Lastname = "last",
                Age = 34,
                TaxFile = new Core.TaxFile()
                {
                    EmployeeId = 1,
                    Alias = "taxfile",
                    TaxFileRecords = new List<Core.TaxFileRecord>()
                    {
                        new Core.TaxFileRecord() { Id = 1, TaxFileId = 1, AmountPaid = 300, AmountClaimed= 200, FinancialYear = 2022 },
                        new Core.TaxFileRecord() { Id = 2, TaxFileId = 1, AmountPaid = 33, AmountClaimed= 50 , FinancialYear = 2020}
                    },
                }
            };

            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.CreateAsunc() dependency
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);

            in_repo.Setup(x => x.FindAsync(x => true))
                    .Returns(Task.FromResult<List<Core.Employee>>(
                        new List<Core.Employee>() { emp })); // 1 employee

            //ACT
            IResult actual = await routeService.GetAsync();

            //ASSERT
            Assert.IsNotNull(actual);
            var okResult = (Ok<List<Pres.Employee>>)actual;
            Assert.IsNotNull(okResult); // returned without error
            Assert.IsInstanceOfType(okResult, typeof(Ok<List<Pres.Employee>>)); //OK 200 returned
            Assert.IsNotNull(okResult.Value); //content returned
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Pres.Employee>)); //list returned
            Assert.AreEqual(okResult.Value.Count, EXPECTED_EMPLOYEE_COUNT); //1 employees returned
            Assert.IsNotNull(okResult.Value.First().TaxFile); // employee has taxfile
            Assert.IsNotNull(okResult.Value.First().TaxFile!.TaxFileRecords!); //employee has taxfile records
            Assert.AreEqual(okResult.Value.First().TaxFile!.TaxFileRecords!.Count, EXPECTED_TAXFILERECORDS_COUNT);
        }

        [TestMethod()]
        public async Task GetAsync_NotFound_Returns404()
        {
            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.CreateAsunc() dependency
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Employee>(null!));

            //ACT
            IResult actual = await routeService.GetAsync(in_id);

            //ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFound<int>));
        }

        [TestMethod()]
        public async Task GetAsync_Found_ReturnsEmployee()
        {
            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Employee>>(); //mock IRepository.CreateAsunc() dependency
            var in_empService = new EntityService<Core.Employee>(in_repo.Object);
            RouteService<Pres.Employee, Core.Employee> routeService = new RouteService<Pres.Employee, Core.Employee>(this.In_Mapper!, in_empService);
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Employee>(
                    new Core.Employee()
                    {
                        Id = in_id,
                        Firstname = "test",
                        Lastname = "test",
                        Age = 12
                    }));

            //ACT
            IResult actual = await routeService.GetAsync(in_id);

            //ASSERT
            Assert.IsNotNull(actual); // returns result
            var okResult = (Ok<Pres.Employee>)actual;
            Assert.IsNotNull(okResult); //return 200 OK result
            Assert.IsNotNull(okResult.Value); //returns content
            Assert.IsInstanceOfType(okResult.Value, typeof(Pres.Employee)); //returns correct content type
        }
    }
}
