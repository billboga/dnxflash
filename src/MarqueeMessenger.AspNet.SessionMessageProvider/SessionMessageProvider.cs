using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MarqueeMessenger.AspNet.SessionMessageProvider
{
    public class SessionMessageProvider : IMessageProvider
    {
        public SessionMessageProvider(ISessionCollection session)
        {
            this.session = session;
        }

        private readonly ISessionCollection session;
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
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messages))));
        }
    }
}
