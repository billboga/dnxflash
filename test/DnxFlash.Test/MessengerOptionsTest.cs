using Xunit;

namespace DnxFlash.Test
{
    public class MessengerOptionsTest
    {
        [Fact]
        public void Sets_messenger_types()
        {
            var messageTypes = new MessageTypes();

            var sut = new MessengerOptions(messageTypes);

            Assert.Equal(messageTypes, sut.MessageTypes);
        }
    }
}
