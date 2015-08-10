using Microsoft.AspNet.Http.Features;
using Newtonsoft.Json;
using System.Text;

namespace DnxFlash.AspNet.MessageProviders
{
    public class SessionMessageProvider : IMessageProvider
    {
        public SessionMessageProvider(ISession session)
        {
            this.session = session;
        }

        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private readonly ISession session;
        private const string sessionKey = "dnxflash-session-message-provider";

        public object Get()
        {
            byte[] value;
            object messages = null;

            if (session.TryGetValue(sessionKey, out value))
            {
                if (value != null)
                {
                    messages =
                        JsonConvert.DeserializeObject(
                            Encoding.UTF8.GetString(value),
                            jsonSerializerSettings);
                }
            }

            return messages;
        }

        public void Set(object messages)
        {
            session.Set(
                sessionKey,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                    messages,
                    jsonSerializerSettings)));
        }
    }
}
