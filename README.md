# DEPRECATED. Use https://github.com/billboga/motleyflash for anything beyond .NET Core RC1.

# DNXFlash

*compatible with dnx rc1-update1*

Provides a way to queue and display UI-messages between actions (i.e. POST -> GET) leveraging the DNX environment.

## How does it work?

DNXFlash follows a design-principle known as "flash messaging". This allows the implementator to create one-time-use messages for UI-consumption. Messages are added to a queue and are designed to be retained between requests. Once the message is consumed it is removed from the queue.

The library has three key aspects: `Message`, `IMessenger`, and `IMessageProvider`. The first-item holds properties related to the individual message, the second-item controls how messages are stored and retrieved (i.e. FIFO or LIFO), while the last-item deals with how the collection of messages are stored (i.e. session or in-memory).

## What is the use case?

A common MVC-pattern is to `post` a `form` to an endpoint, process the data, then redirect the request to another endpoint via a `get`-request (i.e. [Post-Redirect-Get](https://en.wikipedia.org/wiki/Post/Redirect/Get)). Using DNXFlash, a message can be created within the `post`-action and retained until it is consumed by the latter `get`-request.

Another example is to create a series of messages (maybe just information or log-related) during an action and then displaying them based on some ordering (FIFO or LIFO).

Lastly, since the library is not tied to web applications, it could also be used in console or desktop applications.

## Examples

### MVC6

#### Startup.cs

```csharp
using DnxFlash;
using DnxFlash.AspNet.MessageProviders;

// Boilerplate...

public void ConfigureServices(IServiceCollection services)
{
    // These two lines are needed if you want to use the `SessionMessageProvider`.
    services.AddCaching();
    services.AddSession();

    services.AddScoped<IMessageProvider>(x =>
    {
        return new SessionMessageProvider(x.GetService<IHttpContextAccessor>().HttpContext.Session);
    });

    // We are using Bootstrap v3 and need to provide a custom-value for the error message-type.
    services.AddScoped<IMessageTypes>(x =>
    {
        return new MessageTypes(error: "danger");
    });

    services.AddScoped<IMessengerOptions>(x =>
    {
        return new MessengerOptions(x.GetService<IMessageTypes>());
    });

    // We are using a stack to hold messages (i.e. LIFO).
    services.AddScoped<IMessenger>(x =>
    {
        return new StackMessenger(
            x.GetService<IMessageProvider>(),
            x.GetService<IMessengerOptions>());
    });
}
```

#### HomeController.cs

```csharp
using DnxFlash;
using DnxFlash.Extensions;

// Boilerplate...

public class HomeController : Controller
{
    public HomeController(IMessenger messenger)
    {
        this.messenger = messenger;
    }

    private readonly IMessenger messenger;

    public IActionResult Index()
    {
        // Leveraging extension method.
        messenger.Success(
            text: $"Hello at {DateTimeOffset.UtcNow}.",
            title: "Welcome to DNXFlash");

        // Or, if you want to construct the message manually.
        //messenger.Add(new Message(
        //    text: $"Hello at {DateTimeOffset.UtcNow}.",
        //    title: "Welcome to DNXFlash"));

        return View();
    }
}
```

#### Views/Shared/Components/DnxFlash/Default.cshtml

```csharp
@model IEnumerable<DnxFlash.Message>

@foreach (var message in Model)
{
    <div class="alert alert-@message.Type.ToLower()" role="alert">
        @if (!string.IsNullOrWhiteSpace(message.Title))
        {
            <strong>@message.Title</strong><text>&nbsp;</text>
        }
        @message.Text
    </div>
}
```

#### _ViewImports.cshtml

```csharp
@using DnxFlash.AspNet.Razor.ViewHelpers
@using DnxFlash.AspNet.Razor.ViewHelpers.Extensions
```

#### Index.cshtml (or _Layout.cshtml)

The view is using an extension method to leverage the `DnxFlashViewComponent`. By default, this will use `Default.cshtml`. You can also pass a view-name.

```csharp
@Component.DnxFlash()
```

#### Index.cshtml (Alternative)

```csharp
@Component.Invoke("DnxFlash")
```
