using System;
using AutoFixture;
using BeerQuest.API.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace BeerQuest.API.UnitTests.Infrastructure
{
    [TestFixture]
    class ConnectionStringResolverTests
    {
        private Fixture _fixture;
        private Mock<IConfiguration> _configurationMock;
        private ConnectionStringResolver _connectionStringResolver;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _configurationMock = new Mock<IConfiguration>();
            _connectionStringResolver = new ConnectionStringResolver(_configurationMock.Object);
        }

        [Test]
        public void Ctor_WhenConfigurationNull_ThrowsException()
        {
            Func<ConnectionStringResolver> act = () => new ConnectionStringResolver(null);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("configuration");
        }

        [Test]
        public void Resolve_ReturnsConnectionString()
        {
            var connectionString = _fixture.Create<string>();
            var configurationSectionMock = new Mock<IConfigurationSection>();

            configurationSectionMock.SetupGet(g => g["BeerQuestDb"]).Returns(connectionString);
            _configurationMock.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionMock.Object);

            var result = _connectionStringResolver.Resolve();
            result.Should().Be(connectionString);
        }
    }
}
