namespace MarqueeMessenger
{
    public interface IMessenger
    {
        IMessengerOptions Options { get; }

        IMessenger Add(Message message);
        Message Fetch();
    }
}
