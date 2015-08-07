using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Text;
using Microsoft.AspNet.Http.Features;

namespace MarqueeMessenger.AspNet.MessageProviders
{
    public class SessionMessageProvider : IMessageProvider
    {
        public SessionMessageProvider(ISession session)
        {
            this.session = session;
        }

        private readonly ISession session;
        private const string sessionKey = "marquee-messenger-session-message-provider";

        public object Get()
        {
            byte[] value;
            object messages = null;

            if (session.TryGetValue(sessionKey, out value))
            {
                if (value != null)
                {
                    messages =
                        JsonConvert.DeserializeObject(Encoding.UTF8.GetString(value));
                }
            }

            return messages;
        }

        public void Set(object messages)
        {
            session.Set(
                sessionKey,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messages)));
        }
    }
}
