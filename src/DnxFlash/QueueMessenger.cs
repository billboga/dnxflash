using System;
using System.Collections.Generic;
using System.Linq;

namespace DnxFlash
{
    public class QueueMessenger : IMessenger
    {
        public QueueMessenger(
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

            var providerMessages = messageProvider.Get() as Queue<Message>
                ?? new Queue<Message>();

            var messages = SetMessageOrder(providerMessages);

            messages.Enqueue(message);
            messageProvider.Set(messages);

            return this;
        }

        public Message Fetch()
        {
            var providerMessages = messageProvider.Get() as Queue<Message>
                ?? new Queue<Message>();

            var messages = SetMessageOrder(providerMessages);

            Message message = null;

            if (messages.Count > 0)
            {
                message = messages.Dequeue();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Queue<Message> SetMessageOrder(IEnumerable<Message> unorderedMessages)
        {
            var messages = new Queue<Message>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }

    }
}
