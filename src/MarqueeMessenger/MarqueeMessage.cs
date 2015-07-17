using System;

namespace MarqueeMessenger
{
    public class MarqueeMessage
    {
        public MarqueeMessage(
            string message,
            string title = null,
            string type = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            Message = message;
            Title = title;
            Type = type;
        }

        public string Message { get; private set; }
        public string Title { get; private set; }
        public string Type { get; private set; }
    }
}
