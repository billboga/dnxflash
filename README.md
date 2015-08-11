# DNXFlash

Provides a way to queue and display UI-messages between requests (i.e. POST -> GET) and exceptions leveraging the DNX environment.

## How does it work?

DNXFlash follows a design-principle known as "flash messaging". This allows the implementator to create one-time-use messages for UI-consumption. Messages are added to a queue and are designed to be retained between requests. Once the message is consumed it is removed from the queue.

## What is the use case?

A common MVC-pattern is to `post` a `form` to an endpoint, process the data, then redirect the request to another endpoint via a `get`-request (i.e. Post-Redirect-Get). Using DNXFlash, a message can be created within the `post`-action and retained until it is consumed by the latter `get`-request.

Another example is to create a series of messages (maybe just information or log-related) during an action and then displaying them based on some ordering (FIFO or LIFO).

## TODO Examples