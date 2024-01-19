using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Naru_Shortner.Controllers;
using Naru_Shortner.Models;
using Naru_Shortner.Services.IServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naru_Shortner_Tests
{
    [TestFixture]
    public class UrlControllerTests
    {
        private UrlController urlController;
        private Mock<IUrlService> urlService;
        [SetUp]
        public void Setup()
        {
            urlService = new Mock<IUrlService>();
            urlController = new UrlController(urlService.Object);
        }

        [Test]
        public void RdrTo_IdIsNull()
        {
            //Arrange

            //Act
            var result = urlController.RdrTo(null);
            //Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
        [Test]
        public void RdrTo_IdDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            urlService.Setup(x => x.GetUrlById(It.IsAny<int>()))
                .Returns(() => null);
            //Act
            var result = urlController.RdrTo("fjhgdfs");
            //Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
        [Test]
        public void RdrTo_IdExists_RedirectResult()
        {
            //Arrange
            urlService.Setup(x => x.GetUrlById(It.IsAny<int>()))
                .Returns(() => new Url { LongUrl=" "});
            //Act
            var result = urlController.RdrTo("fjhgdfs");
            //Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
        [Test]
        public async Task Create_NotValidUrl()
        {
            //Arrange

            //Act
            var result = await urlController.Create("dfaw");
            var modelStateResult = urlController?.ModelState[""]?.Errors.Any(x => x.ErrorMessage == "Invalid URL");
            //Assert
            Assert.That(modelStateResult, Is.True);
        }
        [Test]
        public async Task Create_AddNotSuccessful()
        {
            //Arrange
            urlService.Setup(x => x.AddUrl(It.IsAny<string>())).Returns(() => Task.FromResult(false));
            urlService.Setup(x => x.CheckUrl(It.IsAny<string>())).Returns(() => true);
            
            //Act
            var result = await urlController.Create("https://github.com/OmarNaru1110/UrlShortner/blob/master/Naru%20Shortner%20Tests/UrlRepositoryTests.cs");
            var modelStateResult = urlController?.ModelState[""]?.Errors.Any(x => x.ErrorMessage == "Something Went Wrong!\nPlease Try Again");
            //Assert
            Assert.That(modelStateResult, Is.True);
        }
        [Test]
        public async Task Create_AddUrlSuccessfully()
        {
            //Arrange
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpContext.Setup(x => x.Request).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Scheme).Returns("http");
            mockHttpRequest.Setup(x => x.Host).Returns(new HostString("localhost"));

            var controllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            urlController.ControllerContext = controllerContext;

            urlService.Setup(x => x.AddUrl(It.IsAny<string>())).Returns(() => Task.FromResult(true));
            urlService.Setup(x => x.CheckUrl(It.IsAny<string>())).Returns(() => true);
            urlService.Setup(x => x.GetUrlByUrl(It.IsAny<string>())).Returns(() => new Url());
            urlService.Setup(x => x.Encode(It.IsAny<int>())).Returns(() => " ");
            //Act
            var result = await urlController.Create("https://github.com/OmarNaru1110/UrlShortner/blob/master/Naru%20Shortner%20Tests/UrlRepositoryTests.cs");
            //Assert
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        }
    }
}
