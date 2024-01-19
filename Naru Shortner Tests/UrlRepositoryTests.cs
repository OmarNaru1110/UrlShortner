using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Naru_Shortner.BLL;
using Naru_Shortner.Repository.Context;
using NUnit.Framework;
using System.Reflection.Metadata.Ecma335;

namespace Naru_Shortner_Tests
{
    [TestFixture]
    public class UrlRepositoryTests
    {
        private UrlRepository urlRepository;
        private DbContextOptionsBuilder options;
        private UrlContext context;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<UrlContext>()
                .UseInMemoryDatabase(databaseName: "urlContext");

            context = new UrlContext(options.Options);

            urlRepository = new UrlRepository(context);

        }
        [TestCase("adsfadf",ExpectedResult = true)]
        public async Task<bool> Add_String_ReturnTrue(string str)
        {
            return await urlRepository.Add(str);
        }
        [Test]
        public async Task Add_String_IsAdded()
        {
            //Arrange

            //Act
            var result = await urlRepository.Add("omar");
            var urlResult = urlRepository.GetByUrl("omar");

            //Assert
            Assert.That(urlResult, Is.Not.Null);
        }
        [TearDown]
        public void Teardown()
        {
            context.Dispose();
        }
    }
}
