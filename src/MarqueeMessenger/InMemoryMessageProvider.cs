namespace MarqueeMessenger
{
    public class InMemoryMessageProvider : IMessageProvider
    {
        private object messages;

        public object Get()
        {
            return messages;
        }

        public void Set(object messages)
        {
            this.messages = messages;
        }
    }
}
