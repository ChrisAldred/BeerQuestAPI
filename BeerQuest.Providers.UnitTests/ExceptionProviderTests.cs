using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BeerQuest.Providers.UnitTests
{
    [TestFixture]
    public class ExceptionProviderTests
    {
        private Fixture _fixture;
        private Mock<IDataRepository> _dataRepositoryMock;
        private ExceptionProvider _exceptionProvider;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _dataRepositoryMock = new Mock<IDataRepository>();
            _exceptionProvider = new ExceptionProvider(_dataRepositoryMock.Object);
        }

        [Test]
        public void Ctor_DataRepositoryNull_ThrowsException()
        {
            Func<ExceptionProvider> func = () => new ExceptionProvider(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("dataRepository");
        }

        [Test]
        public async Task StoreException_PassesCorrectProcName()
        {
            const string expected = "[dbo].[usp_InsertException]";

            await _exceptionProvider.StoreException(_fixture.Create<string>(), _fixture.Create<string>(),
                _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>());

            _dataRepositoryMock.Verify(x => x.ExecuteStoredProcedureAsync(expected, It.IsAny<object>()));
        }

        [Test]
        public async Task StoreException_PassesCorrectParams()
        {
            var expected = new
            {
                System = _fixture.Create<string>(),
                MachineName = _fixture.Create<string>(),
                Request = _fixture.Create<string>(),
                Message = _fixture.Create<string>(),
                StackTrace = _fixture.Create<string>()
            };

            var actual = new List<object>();
            _dataRepositoryMock.Setup(x => x.ExecuteStoredProcedureAsync(It.IsAny<string>(), Capture.In(actual)));
            await _exceptionProvider.StoreException(expected.System, expected.MachineName, expected.Request, expected.Message, expected.StackTrace);
            actual.Single().Should().BeEquivalentTo(expected);
        }
    }
}