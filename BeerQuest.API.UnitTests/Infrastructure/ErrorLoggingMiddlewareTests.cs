using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeerQuest.API.Infrastructure;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace BeerQuest.API.UnitTests.Infrastructure
{
    [TestFixture]
    class ErrorLoggingMiddlewareTests
    {
        private Mock<IExceptionLogger> _exceptionLoggerMock;

        [SetUp]
        public void Setup()
        {
            _exceptionLoggerMock = new Mock<IExceptionLogger>();
        }

        [Test]
        public void Ctor_NextNull_ThrowsException()
        {
            Func<ErrorLoggingMiddleware> func = () => new ErrorLoggingMiddleware(null);
            func.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("next");
        }

        [Test]
        public async Task InvokeAsync_ExceptionLoggerNull_ThrowsException()
        {
            Task RequestDelegate(HttpContext context)
            {
                return Task.CompletedTask;
            }

            var ret = new ErrorLoggingMiddleware(RequestDelegate);

            (await ret.Invoking(x => x.InvokeAsync(new DefaultHttpContext(), null)).Should()
                .ThrowAsync<ArgumentNullException>()).And.ParamName.Should().Be("exceptionLogger");
        }

        [Test]
        public async Task InvokeAsync_CallsNextDelegate()
        {
            var called = false;

            Task RequestDelegate(HttpContext context)
            {
                called = true;
                return Task.CompletedTask;
            }

            var ret = new ErrorLoggingMiddleware(RequestDelegate);

            await ret.InvokeAsync(new DefaultHttpContext(), _exceptionLoggerMock.Object);
            called.Should().BeTrue();
        }

        [Test]
        public async Task InvokeAsync_WhenExceptionIsThrown_LoggerIsCalledBeforeThrowing()
        {
            var exception = new Exception();

            Task RequestDelegate(HttpContext context)
            {
                throw exception;
            }

            var ret = new ErrorLoggingMiddleware(RequestDelegate);

            using (new AssertionScope())
            {
                await ret.Invoking(x => x.InvokeAsync(new DefaultHttpContext(), _exceptionLoggerMock.Object)).Should()
                    .ThrowAsync<Exception>();
                _exceptionLoggerMock.Verify(x => x.LogExceptionAsync(exception), Times.Once);
            }
        }
    }
}
