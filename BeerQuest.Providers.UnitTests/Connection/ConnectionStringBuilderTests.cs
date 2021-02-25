using AutoFixture;
using BeerQuest.Providers.Connection;
using FluentAssertions;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests.Connection
{
    [TestFixture]
    public class ConnectionStringBuilderTests
    {
        private ConnectionStringBuilder _connectionStringBuilder;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _connectionStringBuilder = new ConnectionStringBuilder();
            _fixture = new Fixture();
        }

        [Test]
        public void Build_ReturnsConnectionString()
        {
            var expected = _fixture.Create<string>();
            var result = _connectionStringBuilder.InitializeWith(expected).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void SetApplicationName_WhenApplicationNameAlreadySet_ReturnsOriginalConnectionString()
        {
            var clientName = "testClientName";
            var connectionString = $"application Name={clientName};";

            var result = _connectionStringBuilder.InitializeWith(connectionString).SetApplicationName(clientName)
                .Build();

            result.Should().Be(connectionString);
        }

        [Test]
        public void SetApplicationName_WhenApplicationNameMissing_SetsApplicationNameToClientName()
        {
            var clientName = "testClientName";
            var connectionString = "someString";

            var result = _connectionStringBuilder.InitializeWith(connectionString).SetApplicationName(clientName)
                .Build();

            result.Should().Be($"{connectionString}application Name={clientName};");
        }

        [Test]
        public void SetApplicationName_WhenApplicationNameDoesNotMatch_SetsApplicationNameToClientName()
        {
            var clientName = "testClientName";
            var connectionString = $"application Name=someString;";

            var result = _connectionStringBuilder.InitializeWith(connectionString).SetApplicationName(clientName)
                .Build();

            result.Should().Be($"application Name={clientName};");
        }
    }
}