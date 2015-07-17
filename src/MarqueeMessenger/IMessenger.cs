namespace MarqueeMessenger
{
    public interface IMessenger
    {
        IMessenger Add(MarqueeMessage message);
        MarqueeMessage Fetch();
    }
}
