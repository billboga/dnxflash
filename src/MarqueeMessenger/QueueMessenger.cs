using System.Collections.Generic;

namespace MarqueeMessenger
{
    public class QueueMessenger : IMessenger
    {
        public QueueMessenger(IMessageProvider messageProvider)
        {
            this.messageProvider = messageProvider;
        }

        private readonly IMessageProvider messageProvider;

        public IMessenger Add(MarqueeMessage message)
        {
            var messages = (messageProvider.Get() as Queue<MarqueeMessage>)
                ?? new Queue<MarqueeMessage>();

            messages.Enqueue(message);
            messageProvider.Set(messages);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            var messages = (messageProvider.Get() as Queue<MarqueeMessage>)
                ?? new Queue<MarqueeMessage>();

            var message = messages.Dequeue();
            messageProvider.Set(messages);

            return message;
        }
    }
}
