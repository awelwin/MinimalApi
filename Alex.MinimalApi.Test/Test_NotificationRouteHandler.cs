using Alex.MinimalApi.Service;
using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Alex.MinimalApi.Service.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Core = Alex.MinimalApi.Service.Core;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Test
{
    [TestClass]
    public class Test_NotificationRouteHandler
    {
        IMapper? In_Mapper { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Test_NotificationRouteHandler()
        {
            this.In_Mapper = AutoMapperConfig.ConfigureMaps();
        }

        [TestMethod()]
        public async Task GetNotificationById_NotFound_Returns404()
        {
            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Notification>>();
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Notification>(null!));

            //ACT
            IResult actual = await NotificationRouteHandler.GetNotificationById(
                in_id,
                in_repo.Object,
                this.In_Mapper!);

            //ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(NotFound<int>));
        }

        [TestMethod()]
        public async Task GetNotificationById_Found_ReturnsNotification()
        {

            //ARANGE
            int in_id = 1;
            var in_repo = new Mock<IRepository<Core.Notification>>();
            in_repo.Setup(x => x.GetAsync(in_id))
                .Returns(Task.FromResult<Core.Notification>(new Notification() { Message = "test" }));

            //ACT
            IResult actual = await NotificationRouteHandler.GetNotificationById(
                in_id,
                in_repo.Object,
                this.In_Mapper!);

            //ASSERT
            Assert.IsNotNull(actual); // returns result
            var okResult = (Ok<Pres.Notification>)actual;
            Assert.IsNotNull(okResult); //return 200 OK result
            Assert.IsNotNull(okResult.Value); //returns content
            Assert.IsInstanceOfType(okResult.Value, typeof(Pres.Notification)); //returns correct content type
        }
    }


}