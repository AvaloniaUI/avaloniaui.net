---
Title: Avalonia Binding Priorities
Published: 2015-05-08
Category: Internals
Author: Steven Kirk
---

:::note
This article refers to a historical version of Avalonia and as such may not be up-to-date.
:::


In my previous blog post I said that we'd cover binding priorities, so here we go. In WPF there is something called [Dependency Property Value Precedence][a9fd6f3f] but it's mostly hidden behind the scenes. However, Avalonia has a CSS-style styling system which means that the priorites (or precedence) of bindings become even more important.

To illustrate, lets consider this simple Button template:


```charp
new Style(x => x.OfType<Button>())
{
    Setters = new[]
    {
        // Apply a control template to all Buttons.
        new Setter(Button.TemplateProperty, ControlTemplate.Create<Button>(this.Template)),
    },
},
new Style(x => x.OfType<Button>().Template().Name("border"))
{
    Setters = new[]
    {
        // Set the default background color of the button. This should be
        // overridable by setting the Button.Background property.
        new Setter(Button.BackgroundProperty, new SolidColorBrush(0xffdddddd)),
    },
},
new Style(x => x.OfType<Button>().Class(":pressed").Template().Name("border"))
{
    Setters = new[]
    {
        // When the button is pressed, change the background color.
        new Setter(Button.BorderBrushProperty, new SolidColorBrush(0xffff628b)),
    },
},
```

```charp
private Control Template(Button control)
{
    Border border = new Border
    {
        Name = "border",
        Content = new ContentPresenter
        {
            Name = "contentPresenter",
            [~ContentPresenter.ContentProperty] = control[~Button.ContentProperty],
        },
        [~Border.BackgroundProperty] = control[~Button.BackgroundProperty],
    };

    return border;
}
```

The style snippet here only covers setting the background color of a Button, but what it tells us is:

- All buttons have a default background color
- The background color is overridable by setting Button.Background
- When the button is pressed, it should be displayed with a "pressed" background.

So we have 3 levels of binding, in increasing precedence:

1. The color set in the base style.
2. The color set on the template parent.
3. The color set when the `:pressed` class is present on the Button.

And indeed if you look at the `BindingPriority` enumeration you'll see something very much like that:

```charp
public enum BindingPriority
{
    /// <summary>
    /// A value that comes from an animation.
    /// </summary>
    Animation = -1,

    /// <summary>
    /// A local value.
    /// </summary>
    LocalValue,

    /// <summary>
    /// A triggered style binding.
    /// </summary>
    /// <remarks>
    /// A style trigger is a selector such as .class which overrides a
    /// <see cref="TemplatedParent"/> binding. In this way, a basic control can have
    /// for example a Background from the templated parent which changes when the
    /// control has the :pointerover class.
    /// </remarks>
    StyleTrigger,

    /// <summary>
    /// A binding to a property on the templated parent.
    /// </summary>
    TemplatedParent,

    /// <summary>
    /// A style binding.
    /// </summary>
    Style,

    /// <summary>
    /// The binding is uninitialized.
    /// </summary>
    Unset = int.MaxValue,
}
```

This is implemented in Avalonia using the `PriorityValue` class. A `PriorityValue` object is created for each property in a `AvaloniaObject` that has a value set.

In the original implementation `PriorityValue` this was implemented using a linked list of bindings. When a new binding was added to a property, it was added into the linked list before any other bindings with the same or higher BindingPriority value. The active binding was simply the first binding in the list that didn't return `PerpsexProperty.UnsetValue`. Values that were set using `AvaloniaObject.SetValue` were simply bindings that produced a single value.

However you'll notice I'm speaking in the past tense - this system works well enough for the styling example described at the start of the article, but after a while it became clear that it wasn't enough.

We'll get into that in the [next installment](/blog/2015-07-08-avalonia-binding-priorities-part-2).

[a9fd6f3f]: https://msdn.microsoft.com/en-us/library/ms743230%28v=vs.110%29.aspx "Dependency Property Value Precedence"
