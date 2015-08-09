namespace MarqueeMessenger
{
    public interface IMessenger
    {
        IMessengerOptions Options { get; }

        IMessenger Add(MarqueeMessage message);
        MarqueeMessage Fetch();
    }
}
