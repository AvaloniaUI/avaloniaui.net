---
Title: Avalonia Logical Tree Weirdness
Published: 2016-09-03
Category: Internals
Author: Steven Kirk
---

Avalonia has a similar concept of logical and visual trees to other XAML frameworks such as WPF,
UWP and Silverlight. However when you take a careful look at the logical tree, it can be seen to
function a little strangely at times!

Take `ItemsControl` as an example and imagine you add two `TextBlock`s to `ItemsControl.Items`:

```xml
<ItemsControl>
  <TextBlock>Hello<TextBlock>
  <TextBlock>World<TextBlock>
</ItemsControl>
```

Here things look pretty simple: the logical tree consists of three controls:

- ItemsControl
  - TextBlock
  - TextBlock

If you look at `ItemsControl.GetLogicalChildren()` you will see that it has two logical children:
the `TextBlock`s we added. If you look at the `TextBlock`'s themselves you will see that their
logical parent is the `ItemsControl`. Simple, right?

Similarly if we do the same with a `StackPanel` we'll see the same:

```xml
<StackPanel>
  <TextBlock>Hello<TextBlock>
  <TextBlock>World<TextBlock>
</StackPanel>
```

- ItemsControl
  - TextBlock
  - TextBlock

Ok, easy! Now lets look deeper into the `ItemsControl` visual tree: the controls created by the
control template. A simplified visual tree might look like this:

- ItemsControl
  - ItemsPresenter
    - StackPanel
      - TextBlock
      - TextBlock

Do you see a problem?

The `StackPanel` in the template has the two `TextBlock`s as its children. But hold on, we've
already determined that the `TextBlock`s are children of the `ItemsControl`, not the `StackPanel`!

In this case we have a `TextBlock` that is the logical child of *two* controls. Ok, that's fine you
say, lets just allow that. But what about the `TextBlock`'s `LogicalParent`? There's only a single
logical parent, it wouldn't make sense to have multiple parents - which one is the actual parent?

The way this works in Avalonia is that as suggested above, the `TextBlock`s are in the
`LogicalChildren` collection of *both* the `ItemsControl` and `StackPanel`, *however* the
`TextBlock`'s logical parent is the `ItemsControl`. This makes sense as if you're only looking at
the logical tree outside the `ItemsControl` template, the `StackPanel` effectively doesn't exist.

What this means is that the logical tree isn't commutative - traversing down the logical tree may
not give the same results as traversing it the other way! The visual tree does have this problem
of course so everything is a lot simpler there.
