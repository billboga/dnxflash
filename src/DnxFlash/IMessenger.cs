namespace DnxFlash
{
    public interface IMessenger
    {
        IMessengerOptions Options { get; }

        IMessenger Add(Message message);
        Message Fetch();
    }
}
