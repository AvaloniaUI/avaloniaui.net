---
Title: Selectors
Order: 20
---

## OfType

```xml
<Style Selector="Button">
<Style Selector="local|Button">
```
```csharp
new Style(x => x.OfType<Button>());
new Style(x => x.OfType(typeof(Button)));
```

Selects a control by type. The first example above selects the `Avalonia.Controls.Button` class. To
include a XAML namespace in the type separate the namespace and the type with a `|` character.

This selector does not match derived types. For that, use the [`Is`](#is) selector.

> Note the type of an object is actually determined by looking at its IStyleable.StyleKey property.
  By default this simply returns the type of the current instance, but if, for example, you do want
  your control which inherits from `Button` to be styled as a `Button`, then you can implement the
  `IStyleable.StyleKey` property on your class to return `typeof(Button)`.

## Name

```xml
<Style Selector="#myButton">
<Style Selector="Button#myButton">
```
```csharp
new Style(x => x.Name("myButton"));
new Style(x => x.OfType<Button>().Name("myButton"));
```

Selects a control by its [`Name`](/api/Avalonia.Controls/Control/580076CD) property.

## Class

```xml
<Style Selector=".large">
<Style Selector=":focus">
<Style Selector="Button.large">
<Style Selector="Button.large:focus">
```
```csharp
new Style(x => x.Class("large"));
new Style(x => x.Class(":focus"));
new Style(x => x.OfType<Button>().Class("large"));
new Style(x => x.OfType<Button>().Class("large").Class(":focus"));
```

Selects a control with the specified style classes. Multiple classes should be separated with a
`.` character, or a `:` character in the case of pseudoclasses. If multiple classes are specified
then the control must have all of the requested classes present in order to match.

## Is

```xml
<Style Selector=":is(Button)">
<Style Selector=":is(local|Button)">
```
```csharp
new Style(x => x.Is<Button>());
new Style(x => x.Is(typeof(Button)));
```

This is very similar to the [`OfType`](#ofType) selector except it also matches derived types.

> Again, the type of an object is actually determined by looking at its IStyleable.StyleKey property.

## Child

```xml
<Style Selector="StackPanel > Button">
```
```csharp
new Style(x => x.OfType<StackPanel>().Child().OfType<Button>());
```

A child selector is defined by separating two selectors with a `>` character. This selector matches
_direct children in the logical tree_, so in the example above the selector will match any `Button`
that is a direct logical child of a `StackPanel`.

## Descendant

```xml
<Style Selector="StackPanel Button">
```
```csharp
new Style(x => x.OfType<StackPanel>().Descendant().OfType<Button>());
```

When two selectors are separated by a space, then the selector will match descendants in the
logical tree, so in this case the selector will match any `Button` that is a logical descendant
of a `StackPanel`.

## PropertyEquals

```xml
<Style Selector="Button[IsDefault=true]">
```
```csharp
new Style(x => x.OfType<Button>().PropertyEquals(Button.IsDefaultProperty, true));
```

Matches any control which has the specified property set to the specified value.

## Template

```xml
<Style Selector="Button /template/ ContentPresenter">
```
```csharp
new Style(x => x.OfType<Button>().Template().OfType<ContentPresenter>());
```

Matches a control in a control template. All other selectors listed here work on the logical tree.
If you wish to select a control in a control template then you must use this selector. The example
selects `ContentPresenter` controls in the templates of `Button`s.

## Not

```xml
<Style Selector=":not(.h1)">
<Style Selector="TextBlock:not(.h1)">
```
```csharp
new Style(x => x.Not(y => y.Class("h1")));
new Style(x => x.OfType<TextBlock>().Not(y => y.Class("h1")));
```

Negates an inner selector.
