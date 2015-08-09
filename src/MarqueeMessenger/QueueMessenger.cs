using System;
using System.Collections.Generic;
using System.Linq;

namespace MarqueeMessenger
{
    public class QueueMessenger : IMessenger
    {
        public QueueMessenger(
            IMessageProvider messageProvider,
            IMessengerOptions messengerOptions)
        {
            this.messageProvider = messageProvider;

            Options = messengerOptions;
        }

        private readonly IMessageProvider messageProvider;
        public IMessengerOptions Options { get; }

        public IMessenger Add(MarqueeMessage message)
        {
            message.MessengerOrderId = DateTimeOffset.UtcNow.UtcTicks;

            var providerMessages = messageProvider.Get() as Queue<MarqueeMessage>
                ?? new Queue<MarqueeMessage>();

            var messages = SetMessageOrder(providerMessages);

            messages.Enqueue(message);
            messageProvider.Set(messages);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            var providerMessages = messageProvider.Get() as Queue<MarqueeMessage>
                ?? new Queue<MarqueeMessage>();

            var messages = SetMessageOrder(providerMessages);

            MarqueeMessage message = null;

            if (messages.Count > 0)
            {
                message = messages.Dequeue();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Queue<MarqueeMessage> SetMessageOrder(IEnumerable<MarqueeMessage> unorderedMessages)
        {
            var messages = new Queue<MarqueeMessage>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }

    }
}
