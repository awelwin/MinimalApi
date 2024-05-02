
using Alex.MinimalApi.Service.Application.EndpointHandlers;
using Alex.MinimalApi.Service.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using System.Text;

namespace Alex.MinimalApi.Test
{
    [TestClass]
    public class Test_PublicDocumentRouteHandler
    {
        [TestMethod]
        public async Task CreateDocument_NoFile_ReturnsBadRequest()
        {
            //ARRANGE
            IPublicDocumentService in_service = new Mock<IPublicDocumentService>().Object;

            //ACT
            var actual = await PublicDocumentRouteHandler.CreateDocument(null!, in_service);

            //ASSERT
            Assert.IsInstanceOfType(actual, typeof(BadRequest<string>));
        }

        [TestMethod]
        public async Task CreateDocument_FileGiven_ReturnsCreatedResult()
        {
            //ARRANGE
            const string EXPECTED_DOCUMENT_ID = "99999";

            using (var in_formFile = new MemoryStream())
            {
                using (var in_content = new MemoryStream(Encoding.UTF8.GetBytes("whatever")))
                {
                    var writer = new StreamWriter(in_formFile);
                    writer.Write("fake content");
                    writer.Flush();
                    in_formFile.Position = 0;
                    IFormFile in_file = new FormFile(in_formFile, 0, in_formFile.Length, "id", "fake_filename");

                    var in_service = new Mock<IPublicDocumentService>();
                    in_service.Setup(x => x.CreateAsync(It.IsAny<Stream>())).Returns(Task.FromResult(EXPECTED_DOCUMENT_ID));

                    //ACT
                    var actual = await PublicDocumentRouteHandler.CreateDocument(in_file, in_service.Object);

                    //ASSERT
                    Assert.IsInstanceOfType(actual, typeof(Created));
                    Assert.IsNotNull(((Created)actual).Location);
                    Assert.IsTrue(((Created)actual).Location!.Contains(EXPECTED_DOCUMENT_ID)); //returns resource id of document that was created    
                }
            }
        }
    }
}

