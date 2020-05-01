---
Title: Converting Binding Values
Order: 30
---

## Negating Values

Often you will need to negate a value that you're binding to. A frequent use for this is to show
or hide and control or to enable/disable it. You can negate a binding value by prepending a "bang"
operator: `!`.

For example you might want to show one control when another control is disabled.

```xml
<StackPanel>
  <TextBox Name="input" IsEnabled="{Binding AllowInput}"/>
  <TextBlock IsVisible="{Binding !AllowInput}">Sorry, no can do!</TextBlock>
</StackPanel>
```

Negation also works when binding to non-boolean values. First of all, the value is converted to a
boolean using `Convert.ToBoolean` and the result from this is negated. Because the integer value
`0` is considered `false` and all other integer values are considered `true`, you can use this
to show a message when a collection is empty:

```xml
<Panel>
  <ListBox Items="{Binding Items}"/>
  <TextBlock IsVisible="{Binding !Items.Count}">No results found</TextBlock>
</Panel>
```

A "double-bang" can be used to convert a non-boolean value to a boolean value. For example to
hide a control when a collection is empty:

```xml
<Panel>
  <ListBox Items="{Binding Items}" IsVisible="{Binding !!Items.Count}"/>
</Panel>
```

## Binding Converters

For more advanced conversions, Avalonia supports
[`IValueConverter `](https://docs.microsoft.com/en-gb/dotnet/api/system.windows.data.ivalueconverter?view=netframework-4.7.1)
the same as other XAML frameworks.

> Note: The `IValueConverter` interface is not available in .NET standard 2.0 so we ship our own,
  in the `Avalonia.Data.Converters` namespace.

Usage is identical to other XAML frameworks:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:ExampleApp;assembly=ExampleApp">

  <Window.Resources>
    <local:MyConverter x:Key="myConverter"/>
  </Window.Resources>

  <TextBlock Text="{Binding Value, Converter={StaticResource myConverter}}"/>
</Window>
```

## Built-in Converters

Avalonia supplies a number of built-in value converters for common scenarios:

| Converter | Description |
| ---- | ----------- |
| `StringConverters.IsNullOrEmpty` | Returns `true` if the input string is null or empty |
| `StringConverters.IsNotNullOrEmpty` | Returns `false` if the input string is null or empty |
| `ObjectConverters.IsNull` | Returns `true` if the input is null |
| `ObjectConverters.IsNotNull` | Returns `false` if the input is null |
| `BoolConverters.And` | A multi-value converter that returns `true` if all inputs are true. |

### Examples

Hiding a `TextBlock` if the bound text is null or empty:

```csharp
<TextBlock Text="{Binding MyText}"
           IsVisible="{Binding MyText, Converter={x:Static StringConverters.IsNotNullOrEmpty}}"/>
```

Hiding a `ContentControl` if the bound content is null or empty:

```csharp
<ContentControl Content="{Binding MyContent}"
                IsVisible="{Binding MyContent, Converter={x:Static ObjectConverters.IsNotNull}}"/>
```
