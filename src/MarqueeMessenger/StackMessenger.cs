using System;
using System.Collections.Generic;
using System.Linq;

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
            message.MessengerOrderId = DateTimeOffset.UtcNow.UtcTicks;

            var providerMessages = messageProvider.Get() as Stack<MarqueeMessage>
                ?? new Stack<MarqueeMessage>();

            var messages = SetMessageOrder(providerMessages);

            messages.Push(message);
            messageProvider.Set(messages);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            var providerMessages = messageProvider.Get() as Stack<MarqueeMessage>
                ?? new Stack<MarqueeMessage>();

            var messages = SetMessageOrder(providerMessages);

            MarqueeMessage message = null;

            if (messages.Count > 0)
            {
                message = messages.Pop();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Stack<MarqueeMessage> SetMessageOrder(IEnumerable<MarqueeMessage> unorderedMessages)
        {
            var messages = new Stack<MarqueeMessage>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }
    }
}
