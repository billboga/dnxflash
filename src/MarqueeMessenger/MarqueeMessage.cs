﻿using System;

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

        /// <summary>
        /// This property is intended to allow `IMessenger` implementations
        /// a way to validate ordering of messages when retrieving from `IMessageProvider`.
        /// Do not rely on this property for any purpose outside custom `IMessenger` implementation.
        /// </summary>
        public long MessengerOrderId { get; set; }

        public string Title { get; private set; }
        public string Type { get; private set; }
    }
}
