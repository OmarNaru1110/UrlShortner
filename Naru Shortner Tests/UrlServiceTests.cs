using Moq;
using Naru_Shortner.Helpers;
using Naru_Shortner.Repository.Context;
using Naru_Shortner.Repository.IRepository;
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
    public class UrlServiceTests
    {
        private IUrlService urlService;

        [SetUp]
        public void SetUp() 
        {
            var urlRepository = new Mock<IUrlRepository>();
            urlService= new UrlService(urlRepository.Object);

        }
        [TestCase("https", ExpectedResult = false)]
        [TestCase("qwekorj",ExpectedResult = false)]
        [TestCase("github.com", ExpectedResult = false)]
        [TestCase("google.com/", ExpectedResult = false)]
        [TestCase("",ExpectedResult = false)]
        public bool CheckUrl_ReturnFalse(string url)
        {
            //Arrange

            //Act
            //Assert
            return urlService.CheckUrl(url);

        }
        [TestCase("https://stackoverflow.com/questions/17968426/changing-the-scheme-of-system-uri",ExpectedResult =true)]
        public bool CheckUrl_ReturnTrue(string url)
        {
            //Arrange

            //Act
            //Assert
            return urlService.CheckUrl(url);

        }

        [TestCase(63,ExpectedResult ="_")]
        [TestCase(64,ExpectedResult ="ba")]
        public string Encode_IdLessThanOrEqual64(int id)
        {
            //Arrange
            
            //Act
            //Assert
            return urlService.Encode(id);
        }
        [TestCase(513, ExpectedResult = "ib")]
        public string Encode_IdGreaterThan64(int id)
        {
            //Arrange

            //Act
            //Assert
            return urlService.Encode(id);
        }
        [TestCase(0, ExpectedResult = "a")]
        public string Encode_IdEqualTo0(int id)
        {
            //Arrange

            //Act
            //Assert
            return urlService.Encode(id);
        }
        [TestCase("a",ExpectedResult = 0)]
        [TestCase("ib",ExpectedResult = 513)]
        [TestCase("ba",ExpectedResult = 64)]
        [TestCase("_",ExpectedResult = 63)]
        public int Decode(string url)
        {
            return urlService.Decode(url);
        }

    }
}
