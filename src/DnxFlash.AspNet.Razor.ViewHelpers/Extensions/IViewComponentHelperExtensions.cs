using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace DnxFlash.AspNet.Razor.ViewHelpers.Extensions
{
    public static class IViewComponentHelperExtensions
    {
        public static HtmlString DnxFlash(
            this IViewComponentHelper value,
            string view = null)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                return value.Invoke<DnxFlashViewComponent>();
            }
            else
            {
                return value.Invoke<DnxFlashViewComponent>(view);
            }
        }
    }
}
