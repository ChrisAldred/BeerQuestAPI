using System;
using System.Data;
using AutoFixture.Kernel;
using BeerQuest.Providers.Connection;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests.Connection
{
    [TestFixture]
    class SqlConnectionFactoryTests
    {
        private Mock<IConnectionContextFactory> _connectionContextFactoryMock;
        private Mock<IConnectionStringBuilder> _connectionStringBuilderMock;
        private SqlConnectionFactory _sqlConnectionFactory;

        [SetUp]
        public void Setup()
        {
            _connectionContextFactoryMock = new Mock<IConnectionContextFactory>();
            _connectionStringBuilderMock = new Mock<IConnectionStringBuilder>();

            _sqlConnectionFactory = new SqlConnectionFactory(_connectionContextFactoryMock.Object,
                _connectionStringBuilderMock.Object);
        }

        [Test]
        public void Ctor_WhenConnectionContextFactoryIsNull_ThrowsArgumentNullException()
        {
            Func<SqlConnectionFactory> func = () => new SqlConnectionFactory(null, _connectionStringBuilderMock.Object);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("connectionContextFactory");
        }

        [Test]
        public void Ctor_WhenConnectionStringBuilderIsNull_ThrowsArgumentNullException()
        {
            Func<SqlConnectionFactory> func = () => new SqlConnectionFactory(_connectionContextFactoryMock.Object, null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("connectionStringBuilder");
        }

        [Test]
        public void CreateConnection_WhenConnectionContextReturnsNull_ThrowException()
        {
            _connectionContextFactoryMock.Setup(x => x.GetCurrentConnectionContext())
                .Returns(null as ConnectionContext);

            Func<IDbConnection> func = () => _sqlConnectionFactory.CreateConnection();

            func.Should().Throw<InvalidOperationException>().Which.Message.Should()
                .Be("Could not get context from connection context factory.");
        }

        [Test]
        public void CreateConnection_ConstructSqlConnectionWithCorrectConnectionString()
        {
            var connectionContext = new ConnectionContext("Data Source=someDatabase;");

            _connectionContextFactoryMock.Setup(x => x.GetCurrentConnectionContext()).Returns(connectionContext);

            _connectionStringBuilderMock.Setup(x => x.InitializeWith(connectionContext.ConnectionString))
                .Returns(_connectionStringBuilderMock.Object);
            _connectionStringBuilderMock.Setup(x => x.Build()).Returns(connectionContext.ConnectionString);

            var result = _sqlConnectionFactory.CreateConnection();
            result.ConnectionString.Should().Be(connectionContext.ConnectionString);
        }
    }
}
