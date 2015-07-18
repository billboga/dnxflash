using System.Collections.Generic;

namespace MarqueeMessenger
{
    public class StackMessenger : IMessenger
    {
        public StackMessenger(IMessageProvider messageProvider)
        {
            this.messageProvider = messageProvider;
        }

        private readonly IMessageProvider messageProvider;

        public IMessenger Add(MarqueeMessage message)
        {
            var messages = (messageProvider.Get() as Stack<MarqueeMessage>)
                ?? new Stack<MarqueeMessage>();

            messages.Push(message);
            messageProvider.Set(messages);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            var messages = (messageProvider.Get() as Stack<MarqueeMessage>)
                ?? new Stack<MarqueeMessage>();

            var message = messages.Pop();
            messageProvider.Set(messages);

            return message;
        }
    }
}
