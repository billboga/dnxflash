using Xunit;

namespace MarqueeMessenger.Tests
{
    public class QueueMessengerTest
    {
        public QueueMessengerTest()
        {
            sut = new QueueMessenger(new InMemoryMessageProvider());
        }

        private readonly QueueMessenger sut;

        [Fact]
        public void Should_add_new_items_on_top_of_previous_item_and_fetch_from_bottom()
        {
            sut
                .Add(new MarqueeMessage("first"))
                .Add(new MarqueeMessage("second"));

            var expectedTop = sut.Fetch();
            var expectedBottom = sut.Fetch();

            Assert.NotNull(expectedTop);
            Assert.NotNull(expectedBottom);

            Assert.Equal("first", expectedTop.Message);
            Assert.Equal("second", expectedBottom.Message);
        }
    }
}
