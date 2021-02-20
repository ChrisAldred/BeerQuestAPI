using AutoFixture;
using BeerQuest.Providers.Connection;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests.Connection
{
    [TestFixture]
    public class ConnectionContextFactoryTests
    {
        private Mock<IConnectionStringResolver> _connectionStringResolverMock;
        private ConnectionContextFactory _connectionContextFactory;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _connectionStringResolverMock = new Mock<IConnectionStringResolver>();
            _connectionContextFactory = new ConnectionContextFactory(_connectionStringResolverMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public void GetCurrentConnectionContext_ReturnsCorrectResolverResults()
        {
            var connectionString = _fixture.Create<string>();

            _connectionStringResolverMock.Setup(x => x.Resolve()).Returns(connectionString);

            var result = _connectionContextFactory.GetCurrentConnectionContext();

            result.ConnectionString.Should().Be(connectionString);
        }
    }
}