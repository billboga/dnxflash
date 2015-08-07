namespace MarqueeMessenger
{
    public class InMemoryMessageProvider : IMessageProvider
    {
        protected object messages { get; set; }

        public T Get<T>()
        {
            return (T)messages;
        }

        public void Set(object messages)
        {
            this.messages = messages;
        }
    }
}
