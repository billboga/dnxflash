using System.Collections.Generic;

namespace MarqueeMessenger
{
    public class InMemoryStackMessenger : IMessenger
    {
        public InMemoryStackMessenger()
        {
            messages = new Stack<MarqueeMessage>();
        }

        protected InMemoryStackMessenger(Stack<MarqueeMessage> stack)
        {
            messages = stack;
        }

        private readonly Stack<MarqueeMessage> messages;

        public IMessenger Add(MarqueeMessage message)
        {
            messages.Push(message);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            return messages.Pop();
        }
    }
}
