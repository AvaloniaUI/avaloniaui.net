---
Title: Avalonia Binding Priorities Part 2
Published: 2015-07-08
Category: Internals
Author: Steven Kirk
---

:::note
This article refers to a historical version of Avalonia and as such may not be up-to-date.
:::


In the [last post](/blog/2015-05-08-avalonia-binding-priorities) I described the
first attempt at binding priorites in [Avalonia](https://github.com/grokys/Avalonia/).

To see the problem with this simple system consider the following example:

```charp
var textBox = new TextBox();
textBox.Bind(TextBox.TextProperty, viewModel.WhenAnyValue(x => Text));
textBox.GetObservable(TextBox.TextProperty).Subscribe(x => viewModel.Text = x);
```

This creates a two-way binding between the TextBox.Text property and the
viewModel.Text property. But there's a problem. With the system described in
part 1, *local values are stored as bindings*, as the `PriorityValue` is just
a linked list of bindings.

**Which means that a user typing into the TextBox overrides the binding setup
above.** So a binding would fire only once after user interaction and remain
dormant from then on.

Clearly this isn't desirable behavior.

The solution was that for each priority level, you need a "local value" : a
value set using `AvaloniaObject.SetValue` that takes affect only until the next
time a binding of the same priority fires.

And this is the story of how `PriorityValue` became a collection of
`PriortyLevel`s.

[However, this isn't the end of the story...](/blog/2015-07-08-avalonia-binding-priorities-part-3)
