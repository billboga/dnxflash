namespace MarqueeMessenger
{
    public class MarqueeMessenger : IMessenger
    {
        public MarqueeMessenger(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        private readonly IMessenger messenger;

        public IMessenger Add(MarqueeMessage message)
        {
            messenger.Add(message);

            return this;
        }

        public MarqueeMessage Fetch()
        {
            return messenger.Fetch();
        }
    }
}
