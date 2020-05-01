---
Title: The Avalonia Eventing System
Published: 2015-02-05
Category: Internals
Author: Steven Kirk
---

:::note
This article refers to a historical version of Avalonia and as such may not be up-to-date.
:::

Just about to land on master is an update to the Avalonia eventing system. Avalonia generally follows
WPF quite closely in its eventing, using a routed event model, but this update marks a few
deviations away from that of WPF.

The main features of WPF's routed events are:

* Direct events that work pretty much the same as standard .NET events in that they fire only on the
  control that the event was raised on.
* Tunneling events (`Preview*` events in WPF) which start at the root and tunnel their way up to
  the control that the event was raised on.
* Bubbling events which start at the control that the event was raised on and bubble their way up to
  the root.
* Class handlers which are called before the standard event handlers to invoke the `On*` virtual
  methods in the control classes themselves.

I've recently been diving into Windows Store Apps (aka XAML apps, or WinRT apps, or Metro apps - MS
need to get their naming shit together) and noticed that there, tunneling events are done away with
entirely. This is in some ways a good idea - the `Preview*` events cluttered up class definitions
and intellisense with a load of stuff that must users will never be interested in, but at the same
time as a control author they can be very useful. And they meant that for every event that wants to
be both bubbling and tunneling, you have to declare and call the pair.

In Avalonia I've not gone as far as WinRT (lets just call it WinRT for brevity) and removed them
completely, however they are somewhat hidden in that there are no longer any `Preview*` events.

So lets have a look at how things work in Avalonia.

## Direct Events, Bubbling Events and Class Handers

Direct events (such as `Button.Click`) and bubbling events (such as `KeyDown`) are still available
in the usual manner - there's generally a standard event exposed on the class (i.e.
`Control.KeyDown`) to be used by users of the control, together with an `On*` virtual method in the
class (such as `Control.OnKeyDown`) to be used by control authors.

The virtual methods are called by registering a class handler which in WPF looked something like
this:

```csharp
static MyControl()
{
    EventManager.RegisterClassHandler(typeof(MyControl), MyEvent, new RoutedEventHandler(LocalOnMyEvent));
}

internal static void LocalOnMyEvent(object sender, RoutedEventArgs e)
{
	((MyControl)sender).OnMyEvent(e);
}

virtual void OnMyEvent(RoutedEventArgs e)
{
}
```

However, in Avalonia we can make use of modern C# to make that a little easier.

```csharp
static MyControl()
{
    MyEvent.AddClassHandler<MyControl>(x => x.OnMyEvent));
}

virtual void OnMyEvent(RoutedEventArgs e)
{
}

```

Much nicer! You can pass a RoutingStrategy to that call to AddClassHandler if you want to handle
tunneling events: by default it will handle direct and bubbling events.

## Tunnelling Events

As mentioned earlier there are no longer any `Preview*` tunneling events exposed on Avalonia
controls. That's not to say you couldn't add some to your own controls, but as they're used by a
tiny subset of users, they kept out of the way by default. To subscribe to a tunneling event
you need to call `Interactive.AddHandler` directly:

```csharp
this.topLevel.AddHandler(MyControl.MyEvent, MyHandler, RoutingStrategies.Tunnel);
```

The AddHandler method returns an IDisposable which you can use to end the subscription, or you can
call `Interactive.RemoveHandler`.

## Registering Events

Events are registered more or less the same as they are in WPF, but instead of registering a pair
of `RoutedEvent`s when you want both a bubbling and tunneling event, you only have to register one.
And, like `AvaloniaProperties`, `RoutedEvents` can be strongly typed:

```csharp
public static readonly RoutedEvent<PointerPressEventArgs> PointerPressedEvent =
  RoutedEvent.Register<InputElement, PointerPressEventArgs>(
    "PointerPressed",
    RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
```

## Raising Events

Events are raised using the `Interactive.RaiseEvent` method:

```csharp
RoutedEventArgs click = new RoutedEventArgs
{
    RoutedEvent = Button.ClickEvent
};

this.RaiseEvent(click);
```

## Marking Events Handled

As in WPF, you can mark an event handled to stop its propagation by setting the
`RoutedEventArgs.Handled` property to `true`. You should do this if you have "acted" on the event,
i.e. your button has been clicked so you don't want your parent control to also act on that click.

```csharp
void MyHandler(object sender, RoutedEventArgs e)
{
    e.Handled = true;
}
```


## Advanced Usage

So what if you want to also handle events that have been marked handled? You just need to pass
an extra argument to AddHandler the same as you would in WPF:

```csharp
this.topLevel.AddHandler(MyControl.MyEvent, MyHandler, RoutingStrategies.Bubble, true);
```

What if you want to handle both tunneling and bubbling events, you... you... *weirdo*? Well you can
do that:

```csharp
this.topLevel.AddHandler(
    MyControl.MyEvent,
    MyHandler,
    RoutingStrategies.Tunnel | RoutingStrategies.Bubble);


void MyHandler(object sender, RoutedEventArgs e)
{
    switch (e.Route)
    {
        case RoutingStrategies.Tunnel:
          // Tunneling event
          break;
        case RoutingStrategies.Bubble:
          // Bubbling event
          break;
    }
}
```
