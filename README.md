# DNXFlash

Provides a way to queue and display UI-messages between actions (i.e. POST -> GET) leveraging the DNX environment.

## How does it work?

DNXFlash follows a design-principle known as "flash messaging". This allows the implementator to create one-time-use messages for UI-consumption. Messages are added to a queue and are designed to be retained between requests. Once the message is consumed it is removed from the queue.

The library has three key aspects: `Message`, `IMessenger`, and `IMessageProvider`. The first-item holds properties related to the individual message, the second-item controls how messages are stored and retrieved (i.e. FIFO or LIFO), while the last-item deals with how the collection of messages are stored (i.e. session or in-memory).

## What is the use case?

A common MVC-pattern is to `post` a `form` to an endpoint, process the data, then redirect the request to another endpoint via a `get`-request (i.e. [Post-Redirect-Get](https://en.wikipedia.org/wiki/Post/Redirect/Get)). Using DNXFlash, a message can be created within the `post`-action and retained until it is consumed by the latter `get`-request.

Another example is to create a series of messages (maybe just information or log-related) during an action and then displaying them based on some ordering (FIFO or LIFO).

Lastly, since the library is not tied to web applications, it could also be used in console or desktop applications.

## Examples

TODO
