namespace MarqueeMessenger
{
    public interface IMessageProvider
    {
        T Get<T>();
        void Set(object messages);
    }
}
