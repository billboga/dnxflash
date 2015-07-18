using System;
using Xunit;

namespace MarqueeMessenger.Tests
{
    public class MarqueeMessageTest
    {
        private MarqueeMessage sut;

        public class Constructor : MarqueeMessageTest
        {
            [Fact]
            public void Should_set_message()
            {
                sut = new MarqueeMessage("test message");

                Assert.Equal("test message", sut.Message);
            }

            [Theory,
                InlineData(null),
                InlineData("")]
            public void Should_throw_exception_if_message_is_not_valid(string message)
            {
                var exception = Assert.Throws<ArgumentNullException>(() => new MarqueeMessage(message));

                Assert.Equal("message", exception.ParamName);
            }

            [Fact]
            public void Should_set_title()
            {
                sut = new MarqueeMessage(
                    message: "test message",
                    title: "test");

                Assert.Equal("test", sut.Title);
            }

            [Fact]
            public void Should_set_type()
            {
                sut = new MarqueeMessage(
                    message: "test message",
                    type: "test");

                Assert.Equal("test", sut.Type);
            }
        }
    }
}
