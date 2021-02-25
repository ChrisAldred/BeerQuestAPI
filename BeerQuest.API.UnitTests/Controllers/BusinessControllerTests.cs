using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using BeerQuest.API.Controllers;
using BeerQuest.Models;
using BeerQuest.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BeerQuest.API.UnitTests.Controllers
{
    [TestFixture]
    public class BusinessControllerTests
    {
        private Mock<IBusinessService> _businessServiceMock;
        private Fixture _fixture;
        private BusinessController _businessController;

        [SetUp]
        public void Setup()
        {
            _businessServiceMock = new Mock<IBusinessService>();
            _fixture = new Fixture();
            _businessController = new BusinessController(_businessServiceMock.Object);
        }

        [Test]
        public void Ctor_BusinessServiceNull_ThrowsException()
        {
            Func<BusinessController> func = () => new BusinessController(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("businessService");
        }

        [Test]
        public async Task Get_WhenServiceThrowsException_ReturnBadRequest()
        {
            var exception = _fixture.Create<Exception>();
            _businessServiceMock.Setup(x => x.GetBusinessesAsync(It.IsAny<BusinessRequest>())).Throws(exception);

            var response = await _businessController.Get(_fixture.Create<BusinessRequest>());

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Result.Should().BeOfType<BadRequestObjectResult>();
                var objectResult = response.Result as BadRequestObjectResult;
                Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode) objectResult.StatusCode);
                objectResult.Value.Should().BeEquivalentTo(exception);
            }
        }

        [Test]
        public async Task Get_ReturnBusinesses()
        {
            var expected = _fixture.Create<Businesses>();
            _businessServiceMock.Setup(x => x.GetBusinessesAsync(It.IsAny<BusinessRequest>())).ReturnsAsync(expected);

            var response = await _businessController.Get(_fixture.Create<BusinessRequest>());

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.Result.Should().BeOfType<OkObjectResult>();
                var objectResult = response.Result as OkObjectResult;
                Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)objectResult.StatusCode);
                objectResult.Value.Should().BeEquivalentTo(expected);
            }
        }
    }
}
