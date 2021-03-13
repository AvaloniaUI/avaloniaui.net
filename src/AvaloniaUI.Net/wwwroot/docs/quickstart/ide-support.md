---
Title: IDE Support
Order: 28
---

## JetBrains Rider

The [JetBrains Rider](https://www.jetbrains.com/rider/) IDE has built-in support for Avalonia XAML [starting in 2020.3](https://www.jetbrains.com/rider/whatsnew/2020-3/#version-2020-3-avalonia-support) including first-class support for Avalonia-specific XAML features and custom code inspections.

See the [JetBrains Rider 2020.3 release announcement](https://www.jetbrains.com/rider/whatsnew/2020-3/#version-2020-3-avalonia-support) for more information,.

Rider does not yet provide a visual designer, but this is in development. See [the GitHub project](https://github.com/ForNeVeR/AvaloniaRider) for more information and instructions on how to install.

## Visual Studio

### Avalonia Visual Studio Extension

The [Avalonia for Visual Studio extension](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaforVisualStudio) includes a XAML designer which can be used to show a live preview of the XAML as you're writing it. With the Avalonia for Visual Studio extension installed, double click on an Avalonia XAML file to open it.

### ReSharper for Visual Studio

For Visual Studio and ReSharper users, [ReSharper 2020.3 introduces](https://www.jetbrains.com/resharper/whatsnew/2020-3/#version-2020-3-avalonia-support) built-in code analysis, code completion, navigation, and find usages.

![Visual Studio Designer](images/vs-designer.png)

If your XAML is in a library, Avalonia needs an executable application in order to be able to preview it. Select an executable project from the dropdown on the top right of the designer. Once your project is built, editing the XAML in the editor will cause the preview to update automatically.

:::note
In some cases, due to bugs/limitations in Visual Studio, the Avalonia XAML designer is not shown and instead the WPF designer gets shown. If your XAML file is showing a lot of errors, try right-clicking the file then selecting "Open With..." -> "Avalonia XAML Editor".
:::

## Design-Time Properties

There are a number of properties that can be applied to your controls which will take effect only at design-time. To use these you must add a namespace to your XAML file:

```
xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
```

With the namespace added, the following design-time properties become available:

### d:DesignWidth and d:DesignHeight

The `d:DesignWidth` and `d:DesignHeight` properties apply a width and height to the control being previewed.

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaApplication1.MainWindow">
    Welcome to Avalonia!
</Window>
```

### d:DataContext

The `d:DataContext` property applies a `DataContext` only at design-time. It is recommended that you use this property in conjunction with the `{x:Static}` directive to reference a static property in one of your assemblies:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:dd="clr-namespace:My.Namespace;assembly=MyAssembly"
        d:DataContext="{x:Static dd:DesignData.ExampleViewModel}"
        x:Class="AvaloniaApplication1.MainWindow">
    Welcome to Avalonia!
</Window>
```

```csharp
namespace My.Namespace
{
    public static class DesignData
    {
        public static MyViewModel ExampleViewModel { get; } = new MyViewModel
        {
            // View Model initialization here.
        };
    }
}
```

## Diagnosing Errors

If you're having problems, try enabling verbose logging. To do this:

- Select the to "Tools" -> "Options" menu in Visual Stduio
- Select "Avalonia XAML Editor" from the list
- Select "Verbose" under "Minimum Log Verbosity"

Logs will be displayed in the Visual Studio Output window. Select "Show Output From: Avalonia Diagnostics".
