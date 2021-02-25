using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using BeerQuest.API.Infrastructure;
using BeerQuest.Providers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace BeerQuest.API.UnitTests.Infrastructure
{
    [TestFixture]
    class ExceptionLoggerTests
    {
        private Fixture _fixture;
        private HttpContext _httpContext;
        private Mock<IExceptionProvider> _exceptionProviderMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private ExceptionLogger _exceptionLogger;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _httpContext = new DefaultHttpContext();
            _exceptionProviderMock = new Mock<IExceptionProvider>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _exceptionLogger = new ExceptionLogger(_exceptionProviderMock.Object, _httpContextAccessorMock.Object);

            _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(_httpContext);
        }

        [Test]
        public void Ctor_ExceptionProviderNull_ThrowsException()
        {
            Func<ExceptionLogger> func = () => new ExceptionLogger(null, _httpContextAccessorMock.Object);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("exceptionProvider");
        }

        [Test]
        public void Ctor_HttpContextAccessorNull_ThrowsException()
        {
            Func<ExceptionLogger> func = () => new ExceptionLogger(_exceptionProviderMock.Object, null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("httpContextAccessor");
        }

        [Test]
        public async Task LogExceptionAsync_CallsProvider_WithExpectedSystem()
        {
            await _exceptionLogger.LogExceptionAsync(new Exception());

            _exceptionProviderMock.Verify(x => x.StoreException(
                    ExceptionLogger.System,
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task LogExceptionAsync_CallsProvider_WithExpectedMachineName()
        {
            await _exceptionLogger.LogExceptionAsync(new Exception());

            _exceptionProviderMock.Verify(x => x.StoreException(
                    It.IsAny<string>(),
                    Environment.MachineName,
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task LogExceptionAsync_CallsProvider_WithExpectedRequest()
        {
            _httpContext.Request.Scheme = "http";
            _httpContext.Request.Host = new HostString("localhost");
            _httpContext.Request.Path = "/hello";
            _httpContext.Request.QueryString = QueryString.Create("q", "v");

            await _exceptionLogger.LogExceptionAsync(new Exception());

            _exceptionProviderMock.Verify(x => x.StoreException(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    "http://localhost/hello?q=v",
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task LogExceptionAsync_CallsProvider_WithExpectedMessage()
        {
            var expected = _fixture.Create<string>();

            await _exceptionLogger.LogExceptionAsync(new Exception(expected));

            _exceptionProviderMock.Verify(x => x.StoreException(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    expected,
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public async Task LogExceptionAsync_CallsProvider_WithExpectedStackTrace()
        {
            Exception ex;
            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                ex = e;
            }

            await _exceptionLogger.LogExceptionAsync(ex);

            _exceptionProviderMock.Verify(x => x.StoreException(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    ex.StackTrace),
                Times.Once);
        }
    }
}
