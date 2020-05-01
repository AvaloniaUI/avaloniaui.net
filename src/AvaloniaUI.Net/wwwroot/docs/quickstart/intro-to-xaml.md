---
Title: Introduction to XAML
Order: 20
---
Avalonia uses XAML to define user-interfaces. XAML is an XML-based markup language that is used by
many UI framworks.

> Note: This section is intended as a basic introduction to using XAML in Avalonia. For more
  information see the 
  [Microsoft XAML documentation for WPF](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/xaml-overview-wpf)
  or [Microsoft XAML documentation for UWP](https://docs.microsoft.com/en-us/windows/uwp/xaml-platform/xaml-overview) -
  many of the concepts are the same between frameworks.

## Format of an Avalonia XAML File

A basic Avalonia XAML file looks like this:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow">
</Window>
```

There are three parts to this file:

- The root element `Window` - this descibes the type of the root control in the XAML file; in this
   case [`Window`](/api/Avalonia.Controls/Window/)
- `xmlns="https://github.com/avaloniaui"` - this is the XAML namespace for Avalonia. Without this,
  the file will not be recognised as an Avalonia XAML document.
- `xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"` - this is the XAML-language XAML
  namespace. This isn't strictly necessary, but you will probably need it for accessing certain
  features of the XAML language.
- `x:Class="AvaloniaApplication1.MainWindow"` - this tells the XAML compiler where to find
  the associated class for this file, defined in [code-behind](/docs/quickstart/codebehind)

## Declaring Controls

Controls are added to the XAML by adding an XML element with the control's class name. For example
to add a button as the child of the window you would write:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Button>Hello World!</Button>
</Window>
```

See the [controls documentation](/docs/controls) for a list of the controls included with Avalonia.

## Setting Properties

You can set a property of a control by adding an XML attribute to an element. For example to create
a button with a blue background you could write:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Button Background="Blue">Hello World!</Button>
</Window>
```

You can also use _property element syntax_ for setting properties. For more information see the
[WPF documentation](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/xaml-overview-wpf#property-element-syntax).

## Content Properties

You may notice that the button above has its "Hello World!" content placed directly inside the XML
element. This could also be written as a property using:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Button Content="Hello World"/>
</Window>
```

This is because [`Button.Content`](/api/Avalonia.Controls/ContentControl/4B02A756) is declared as a
_`[Content]` Property_ which means that any content placed inside its XML tag will be assigned to this
property.

## Binding

You can bind a property using the `{Binding}` markup extension:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Button Content="{Binding Greeting}"/>
</Window>
```

For more information, see the [binding documentation](/docs/binding).

## Code-behind

Many XAML files also have an associated _code-behind_ file which usually has the extension
`.xaml.cs`. For more information see the [codebehind documentation](/docs/quickstart/codebehind).
