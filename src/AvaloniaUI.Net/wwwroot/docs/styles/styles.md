---
Title: Styles
Order: 10
---

Styles in Avalonia are used to share property settings between controls. The Avalonia styling
system can be thought of as a mix of CSS styling and WPF/UWP styling. At its most basic, a
style consists of a _selector_ and a collection of _setters_. 

The following style selects any `TextBlock` with a `h1` _style class_ and sets its font size to 24
point and font weight to bold:

```xml
<Style Selector="TextBlock.h1">
    <Setter Property="FontSize" Value="24"/>
    <Setter Property="FontWeight" Value="Bold"/>
</Style>
```

Styles can be defined on any control or on the `Application` object by adding them to the 
[`Control.Styles`](/api/Avalonia.Controls/Control/4145DF25) or 
[`Application.Styles`](/api/Avalonia/Application/04017CAF) collections.

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Window.Styles>
        <Style Selector="TextBlock.h1">
            <Setter Property="FontSize" Value="24"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
    </Window.Styles>

    <TextBlock Classes="h1">I'm a Heading!</TextBlock>
</Window>
```

A style applies to the control that it is defined on and all descendent controls.

:::note
Note that unlike WPF/UWP, styles will have no effect if they're added to a control or application
`ResourceDictionary`. This is because the order that styles are defined in Avalonia is important
and `ResourceDictionary` is an unsorted dictionary.
:::

## Style Classes

As in CSS, controls can be given *style classes* which can be used in selectors. Style classes
can be assigned in XAML by setting the `Classes` property to a space-separated list of strings.
The following example applies the `h1` and `blue` style classes to a `Button`:

```xml
<Button Classes="h1 blue"/>
```

Style classes can also be manipulated in code using the `Classes` collection:

```csharp
control.Classes.Add("blue");
control.Classes.Remove("red");
```

## Pseudoclasses

Also as in CSS, controls can have pseudoclasses; these are classes that are
defined by the control itself rather than by the user. Pseudoclasses start
with a `:` character.

One example of a pseudoclass is the `:pointerover`
pseudoclass (`:hover` in CSS - we may change to that in future).

Pseudoclasses provide the functionality of `Triggers` in WPF and
`VisualStateManager` in UWP:

```xml
<StackPanel>
  <StackPanel.Styles>
    <Style Selector="Button:pointerover">
      <Setter Property="Button.Foreground" Value="Red"/>
    </Style>
  </StackPanel.Styles>
  <Button>I will have red text when hovered.</Button>
</StackPanel>
```

Other pseudoclasses include `:focus`, `:disabled`, `:pressed` for buttons,
`:checked` for checkboxes etc.

## Selectors

_Selectors_ select a control using a custom selector syntax which is very similar to the syntax
used for CSS selectors. An example of some selectors:

|Selector|Description|
|--------|-----------|
|`Button`|Selects all `Button` controls|
|`Button.red`|Selects all `Button` controls with the `red` style class|
|`Button.red.large`|Selects all `Button` controls with the `red` and `large` style classes|
|`Button:focus`|Selects all `Button` controls with the `:focus` pseudoclass|
|`Button.red:focus`|Selects all `Button` controls with the `red` style class and the `:focus` pseudoclass|
|`Button#myButton`|Selects a `Button` control with a name of `myButton`|
|`StackPanel Button.foo`| Selects all `Button`s with the `foo` class that are descendants of a `StackPanel`|
|`StackPanel > Button.foo`| Selects all `Button`s with the `foo` class that are children of a `StackPanel`|

For more information see the  [selectors documentation](/docs/styles/selectors).

## Setters

A style's setters describe what will happen when the selector matches a control. They are simple
property/value pairs written in the format:

```xml
<Setter Property="FontSize" Value="24"/>
<Setter Property="Padding" Value="4 2 0 4"/>
```

Whenever a style is matched with a control, all of the setters will be applied to the control. 
If a style selector should no longer match a control, the property value will revert to its
previous value.

## Style Precedence

If multiple styles match a control, and they both attempt to set the same property then the style
_closest to the control_ will win. Consider the following example:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Window.Styles>
        <Style Selector="TextBlock.h1">
            <Setter Property="FontSize" Value="24"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
    </Window.Styles>

    <StackPanel>
        <StackPanel.Styles>
            <Style Selector="TextBlock.h1">
                <Setter Property="FontSize" Value="48"/>
                <Setter Property="Foreground" Value="Red"/>
            </Style>
        </StackPanel.Styles>

        <TextBlock Classes="h1">
            <StackPanel.Styles>
                <Style Selector="TextBlock.h1">
                    <Setter Property="Foreground" Value="Blue"/>
                </Style>
            </StackPanel.Styles>

            I'm a Heading!
        </TextBlock>
    </StackPanel>
</Window>
```

Here the `h1` style is defined in multiple places. The `TextBlock` will end up with the following
settings:

|Property|Value|Source|
|--------|-----|------|
|`FontSize`|48|`StackPanel`|
|`FontWeight`|Bold|`Window`|
|`Foreground`|Blue|`TextBlock`|

If more than one style setter applies to a property, the value that takes precedence will be:

- The value from the style defined in the ancestor closest to the control
- For two styles declared in the same `Styles` collection, the style that appears later
