using System;
using System.Threading.Tasks;
using AutoFixture;
using BeerQuest.Models;
using BeerQuest.Providers;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Services.UnitTests
{
    [TestFixture]
    public class BusinessServiceTests
    {
        private Mock<IBusinessProvider> _businessProviderMock;
        private Fixture _fixture;
        private BusinessService _businessService;

        [SetUp]
        public void Setup()
        {
            _businessProviderMock = new Mock<IBusinessProvider>();
            _fixture = new Fixture();
            _businessService = new BusinessService(_businessProviderMock.Object);
        }

        [Test]
        public void Ctor_BusinessProviderNull_ThrowsException()
        {
            Func<BusinessService> func = () => new BusinessService(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("businessProvider");
        }

        [Test]
        public async Task GetBusinessesAsync_ReturnsBusinesses()
        {
            var expected = _fixture.Create<Businesses>();

            _businessProviderMock.Setup(x => x.GetBusinessesAsync(It.IsAny<BusinessRequest>())).ReturnsAsync(expected);

            var result = await _businessService.GetBusinessesAsync(_fixture.Create<BusinessRequest>());
            result.Should().NotBeNull().And.BeOfType<Businesses>().And.BeEquivalentTo(expected);
        }
    }
}
