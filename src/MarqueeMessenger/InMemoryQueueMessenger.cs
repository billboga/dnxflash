using System.Collections.Generic;

namespace MarqueeMessenger
{
    public class InMemoryQueueMessenger : IMessenger
    {
        public InMemoryQueueMessenger()
        {
            messages = new Queue<MarqueeMessage>();
        }

        private readonly Queue<MarqueeMessage> messages;

        public IMessenger Add(MarqueeMessage message)
        {
            messages.Enqueue(message);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            return messages.Dequeue();
        }
    }
}
