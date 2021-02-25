using AutoFixture;
using BeerQuest.Providers.Connection;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BeerQuest.Providers.UnitTests.Connection
{
    [TestFixture]
    public class ConnectionContextTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Ctor_ConnectionStringNull_ThrowsExeption()
        {
            Func<ConnectionContext> act = () => new ConnectionContext(null);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("connectionString");
        }

        [Test]
        public void Ctor_CorrectlySetsFields()
        {
            var connectionString = _fixture.Create<string>();

            var connectionContext = new ConnectionContext(connectionString);

            connectionContext.ConnectionString.Should().Be(connectionString);
        }
    }
}
