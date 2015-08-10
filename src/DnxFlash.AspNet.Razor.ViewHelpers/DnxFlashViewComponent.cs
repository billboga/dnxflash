using Microsoft.AspNet.Mvc;
using System.Collections.Generic;

namespace DnxFlash.AspNet.Razor.ViewHelpers
{
    public class DnxFlashViewComponent : ViewComponent
    {
        public DnxFlashViewComponent(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        private readonly IMessenger messenger;

        public IViewComponentResult Invoke()
        {
            return Invoke(null);
        }

        public IViewComponentResult Invoke(string view)
        {
            var messages = new List<Message>();
            Message message = null;

            while ((message = messenger.Fetch()) != null)
            {
                messages.Add(message);
            }

            return View(view, messages);
        }
    }
}
