---
Title: Avalonia Binding Priorities Part 3
Published: 2015-07-08
Category: Internals
Author: Steven Kirk
---

:::note
This article refers to a historical version of Avalonia and as such may not be up-to-date.
:::


In the [second post of the series](/blog/2015-07-08-avalonia-binding-priorities-part-2)
I described how binding priorites in [Avalonia](https://github.com/grokys/Avalonia/)
were fixed in the light of problems with local values. However, a problem still
remained and it showed itself up in the `DropDown` control.

`DropDown` contains in its template, among other things, a `ToggleButton` and
a `Popup`. On the `DropDown` itself there is the `IsDropDownOpen` property
which - as the name suggests - is true when the popup is showing.

The `IsDropDownOpen` property is bound two-way with both the
`ToggleButton.IsChecked` and the `Popup.IsOpen` properties such that any of
the following causes the popup to be shown or hidden:

- Setting `DropDown.IsDropDownOpen`
- Setting `ToggleButton.IsChecked`
- Setting `Popup.IsOpen`

The problem arises because *two* properties are bound to `DropDown.IsDropDownOpen`
but *neither of them should take precedence*. In the previously described
system, newer added bindings take precedence of later added bindings. This is
desired behavior for styles, where styles closer to the control should override
those further up the tree, but here it breaks `DropDown.IsDropDownOpen`.

The problem can also be seen in the following:

```charp
var tb1 = new TextBox
{
    Text = "A non-wrapping text box. Lorem ipsum dolor sit amet.",
    TextWrapping = TextWrapping.NoWrap,
};

var tb2 = new TextBox
{
    Text = "A non-wrapping text box. Lorem ipsum dolor sit amet.",
    TextWrapping = TextWrapping.NoWrap,
};

var tb3 = new TextBox
{
    Text = "A non-wrapping text box. Lorem ipsum dolor sit amet.",
    TextWrapping = TextWrapping.NoWrap,
};

tb2.BindTwoWay(TextBox.TextProperty, tb1, TextBox.TextProperty);
tb2.BindTwoWay(TextBox.TextProperty, tb3, TextBox.TextProperty);
```

Here, `tb2` should be bound to the Text property of both `tb1` and `tb2` and so
all three TextBoxes should stay in sync, however the binding from `tb3` was
overriding that from tb1 causing the TextBoxes to get out-of-sync.

The solution I came up with was:

*Binding levels with a an even number priority use the last fired binding
for the current value, those with odd number priorities use the latest
added.*

This way for local values and template bindings the last fired binding takes
precedence, whereas for styles and animations, the latest added binding takes
precedence. This fitted strangely well into the ordering of the
`BindingPriority` enum - I wonder what could be wrong this time...
