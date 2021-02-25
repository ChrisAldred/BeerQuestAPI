using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using BeerQuest.Providers.Connection;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests
{
    [TestFixture]
    public class DataRepositoryTests
    {
        private Mock<IConnectionFactory> _connectionFactoryMock;
        private Mock<IDbConnection> _dbConnectionMock;
        private Mock<DbCommand> _stubDbCommandMock;
        private DataRepository _dataRepository => new DataRepository(_connectionFactoryMock.Object);

        [SetUp]
        public void Setup()
        {
            _stubDbCommandMock = new Mock<DbCommand>
            {
                DefaultValue = DefaultValue.Mock
            };

            _dbConnectionMock = new Mock<IDbConnection>();
            _dbConnectionMock.SetupGet(x => x.State).Returns(ConnectionState.Open);
            _dbConnectionMock.Setup(x => x.CreateCommand()).Returns(_stubDbCommandMock.Object);

            _stubDbCommandMock.Setup(x => x.ExecuteScalarAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Mock.Of<IConvertible>());

            _connectionFactoryMock = new Mock<IConnectionFactory>();
            _connectionFactoryMock.Setup(x => x.CreateConnection()).Returns(_dbConnectionMock.Object);
        }

        [Test]
        public void Ctor_NullConnectionFactory_ThrowsException()
        {
            Func<DataRepository> func = () => new DataRepository(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("connectionFactory");
        }

        [Test]
        public void Ctor_CreatesConnectionViaFactory_AndOpens()
        {
            _ = _dataRepository;

            using (new AssertionScope())
            {
                _connectionFactoryMock.Verify(x => x.CreateConnection(), Times.Once);
                _dbConnectionMock.Verify(x => x.Open(), Times.Once);
            }
        }

        [Test]
        public void Dispose_DisposesConnection()
        {
            var dbConnection = new Mock<IDisposable>();
            _connectionFactoryMock.Setup(x => x.CreateConnection()).Returns(dbConnection.As<IDbConnection>().Object);

            using (_ = _dataRepository)
            {
            }

            dbConnection.Verify(x => x.Dispose(), Times.Once);
        }
    }
}