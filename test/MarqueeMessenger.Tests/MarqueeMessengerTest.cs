using Moq;
using Xunit;

namespace MarqueeMessenger.Tests
{
    public class MarqueeMessengerTest
    {
        public MarqueeMessengerTest()
        {
            messengerMock = new Mock<IMessenger>();
            sut = new MarqueeMessenger(messengerMock.Object);
        }

        private readonly Mock<IMessenger> messengerMock;
        private readonly MarqueeMessenger sut;

        public class Add : MarqueeMessengerTest
        {
            [Fact]
            public void Should_add_message_via_messenger()
            {
                sut.Add(new MarqueeMessage("test message"));

                messengerMock.Verify(x => x.Add(It.IsAny<MarqueeMessage>()), Times.Once());
            }

            [Fact]
            public void Should_fetch_message_via_messenger()
            {
                sut.Add(new MarqueeMessage("test message"));

                sut.Fetch();

                messengerMock.Verify(x => x.Fetch(), Times.Once());
            }
        }
    }
}
