using System;
using System.Collections.Generic;
using System.Linq;

namespace MarqueeMessenger
{
    public class StackMessenger : IMessenger
    {
        public StackMessenger(
            IMessageProvider messageProvider,
            IMessengerOptions messengerOptions)
        {
            this.messageProvider = messageProvider;

            Options = messengerOptions;
        }

        private readonly IMessageProvider messageProvider;
        public IMessengerOptions Options { get; }

        public IMessenger Add(Message message)
        {
            message.MessengerOrderId = DateTimeOffset.UtcNow.UtcTicks;

            var providerMessages = messageProvider.Get() as Stack<Message>
                ?? new Stack<Message>();

            var messages = SetMessageOrder(providerMessages);

            messages.Push(message);
            messageProvider.Set(messages);

            return this;
        }

        public Message Fetch()
        {
            var providerMessages = messageProvider.Get() as Stack<Message>
                ?? new Stack<Message>();

            var messages = SetMessageOrder(providerMessages);

            Message message = null;

            if (messages.Count > 0)
            {
                message = messages.Pop();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Stack<Message> SetMessageOrder(IEnumerable<Message> unorderedMessages)
        {
            var messages = new Stack<Message>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }
    }
}
