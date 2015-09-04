using Xunit;

namespace DnxFlash.Test
{
    public class StackMessengerTest
    {
        public StackMessengerTest()
        {
            sut = new StackMessenger(
                new InMemoryMessageProvider(),
                new MessengerOptions(new MessageTypes()));
        }

        private readonly StackMessenger sut;

        [Fact]
        public void Should_add_new_items_on_top_of_previous_item_and_fetch_from_top()
        {
            sut
                .Add(new Message("first"))
                .Add(new Message("second"))
                .Add(new Message("third"));

            var expectedTop = sut.Fetch();
            var expectedBottom = sut.Fetch();

            Assert.NotNull(expectedTop);
            Assert.NotNull(expectedBottom);

            Assert.Equal("third", expectedTop.Text);
            Assert.Equal("second", expectedBottom.Text);
        }

        public class Fetch : StackMessengerTest
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
