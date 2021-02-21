using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using BeerQuest.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests
{
    [TestFixture]
    public class BusinessProviderTests
    {
        private Fixture _fixture;
        private Mock<IDataRepository> _dataRepositoryMock;
        private BusinessProvider _businessProvider;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dataRepositoryMock = new Mock<IDataRepository>();
            _businessProvider = new BusinessProvider(_dataRepositoryMock.Object);
        }

        [Test]
        public void Ctor_DataRepositoryNull_ThrowsException()
        {
            Func<BusinessProvider> func = () => new BusinessProvider(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("dataRepository");
        }

        [Test]
        public async Task GetBusinessesAsync_PassesCorrectSql()
        {
            await _businessProvider.GetBusinessesAsync(_fixture.Create<BusinessRequest>());

            _dataRepositoryMock.Verify(x => x.ExecuteStoredProcedureAsync<Business>("usp_GetBusinesses",
                It.IsAny<object>()));
        }

        [Test]
        public async Task GetBusinessesAsync_PassesCorrectParamObject()
        {
            var expected = _fixture.Create<BusinessRequest>();
            var actual = new List<object>();

            _dataRepositoryMock.Setup(x => x.ExecuteStoredProcedureAsync<Business>(It.IsAny<string>(),
                Capture.In(actual)));

            await _businessProvider.GetBusinessesAsync(expected);
            actual.First().Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetBusinessesAsync_ReturnsBusinesses()
        {
            var businessList = _fixture.CreateMany<Business>().ToList();
            var businessRequest = _fixture.Create<BusinessRequest>();
            var expected = new Businesses
            {
                Offset = businessRequest.Offset,
                Limit = businessRequest.Limit,
                BusinessCollection = businessList
            };

            _dataRepositoryMock.Setup(x => x.ExecuteStoredProcedureAsync<Business>(It.IsAny<string>(),
                It.IsAny<object>()))
                .ReturnsAsync(businessList);

            var response = await _businessProvider.GetBusinessesAsync(businessRequest);

            response.Should().NotBeNull().And.BeOfType<Businesses>().And.BeEquivalentTo(expected);
        }
    }
}