using Xunit;

namespace DnxFlash.Test
{
    public class QueueMessengerTest
    {
        public QueueMessengerTest()
        {
            sut = new QueueMessenger(
                new InMemoryMessageProvider(),
                new MessengerOptions(new MessageTypes()));
        }

        private readonly QueueMessenger sut;

        [Fact]
        public void Should_add_new_items_on_top_of_previous_item_and_fetch_from_bottom()
        {
            sut
                .Add(new Message("first"))
                .Add(new Message("second"));

            var expectedTop = sut.Fetch();
            var expectedBottom = sut.Fetch();

            Assert.NotNull(expectedTop);
            Assert.NotNull(expectedBottom);

            Assert.Equal("first", expectedTop.Text);
            Assert.Equal("second", expectedBottom.Text);
        }

        public class Fetch : QueueMessengerTest
        {
            [Fact]
            public void Should_return_null_if_no_messages_left()
            {
                var expected = sut.Fetch();

                Assert.Null(expected);
            }
        }
    }
}
