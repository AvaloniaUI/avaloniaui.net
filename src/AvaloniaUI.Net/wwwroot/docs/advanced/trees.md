---
Title: Logical and Visual Trees
---

When you create a user interface in Avalonia, you are creating a tree of controls, for example:

![Logical Tree](images/logical-tree.png)

The tree is known as the Logical Tree. Controls in Avalonia however are made up of other, more primitive controls. A `CheckBox` for example might consist of a border, a check mark and a piece of text. These controls also make up a tree, which is called the Visual Tree.

![Logical Tree](images/logical-visual-tree.png)

## Logical Tree

The logical tree contains the controls that you place in a `Window` or `UserControl`, together with controls that are created from [`DataTemplate`s](/docs/templates/datatemplate) and containers created by `ListBox`, `TreeView` etc. In addition:

- Inheritable `AvaloniaProperty` values are inherited along the logical tree
- Styles are applied along the logical tree
- Events are propagated along the logical tree
- Control name lookup is done along the logical tree

The logical tree can be traversed by the following means:

- A control's logical parent is exposed via the `Parent` property
- The logical children of a control are stored in the protected `LogicalChildren` collection for derived controls
- Casting a control to `ILogical` exposes a number of properties that expose the logical tree
- A set of [extension methods](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Styling/LogicalTree/LogicalExtensions.cs) in the `Avalonia.LogicalTree` namespace that provide LINQ-style traversal

:::note
Note that although controls can be added to or removed from the `LogicalChildren` collection by derived classes, this can be rather error-prone. It's probably best to derive from one of the existing controls that handle this for you, such as:

- `Decorator` for a control that hosts a single child control
- `Panel` for a control that hosts multiple child controls
- `ContentControl` for a control that needs to display data according to a `DataTemplate`
:::

:::note
The logical tree can behave in unintuitive ways at times. See [this blog post](https://grokys.github.io/avalonia/logical-tree-weirdness/) for some gotchas to look out for when working with it.
:::

## Visual Tree

The visual tree contains the controls in the logical tree, plus the controls that are created by a templated control's `ControlTemplate`. In addition:

- The application is rendered by traversing the visual tree
- Layout is carried out along the visual tree
- Hit testing is done along the visual tree

The visual tree can be traversed by the following means:

- Casting a control to `IVisual` exposes a number of properties that expose the visual tree
- A set of [extension methods](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Visuals/VisualTree/VisualExtensions.cs) in the `Avalonia.VisualTree` namespace that provide LINQ-style traversal
