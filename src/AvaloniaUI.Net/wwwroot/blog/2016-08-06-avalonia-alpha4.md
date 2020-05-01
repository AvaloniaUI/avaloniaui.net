---
Title: Avalonia Alpha 3
Published: 2016-08-06
Category: Release
Author: Steven Kirk
---

We're pleased to announce that alpha 4 of
[Avalonia](https://github.com/AvaloniaUI/Avalonia) is now available.

Avalonia is a cross platform .NET UI framework inspired by WPF, with XAML, data
binding, lookless controls and much more. Avalonia is the only way to bring
XAML-based applications to Windows, Mac and Linux (and we have experimental
mobile support too!)

This is the first release of Avalonia after it was renamed from Avalonia! We did
this due to potential trademark issues with the name Avalonia.

Get the release through [NuGet](https://www.nuget.org/packages/avalonia) and
[download our Visual Studio extension](https://visualstudiogallery.msdn.microsoft.com/e1c6ae1f-6fd9-467d-8f62-1e28b4225213).
You can read our (unfortunately very brief atm) [getting started instructions here](https://github.com/AvaloniaUI/Avalonia/blob/master/docs/tutorial/gettingstarted.md)

Here's the big features in this release - a lot of improvements have been made
everywhere but these are the big hitters:

## List Virtualization

Avalonia now has item virtualization, which means that working with large lists
is now feasible. Previously, when binding a `ListBox` to a list of e.g. 10,000
items, then `ListBox` would create a `ListBoxItem` for every single item, which
was very slow. Now by default virtualization is enabled on `ListBox` meaning
it will only create `ListBoxItem`s for the items that are currently visible.

You don't need to do anything to enable this behavior - it's enabled by
default - but you can turn it off by setting `VirtualizationMode="None"` on the
`ListBox`.

## Per-monitor DPI Support on Windows

Avalonia applications on Windows will now scale their contents to the DPI of the
monitor that they're being displayed on and the window will automatically change
DPI when it's dragged to another monitor with different DPI settings.

## Style Resources

You can now include resources such as brushes and colors in styles so that they
don't need to be hardcoded each time they're used:

```xml
<Style>
  <Style.Resources>
    <SolidColorBrush x:Key="ThemeBackgroundBrush">#FFFFFFFF</SolidColorBrush>
    <sys:Double x:Key="ThemeBorderThickness">2</sys:Double>
  </Style.Resources>
</Style>
```

This differs somewhat from the standard XAML `Resources` collection which is
defined at a control level rather than a style level. To read about the
reasoning behind this, see [this issue comment](https://github.com/AvaloniaUI/Avalonia/issues/462#issuecomment-191849723).

## Skia backend

Work is underway to add a Skia backend using [Skia#](https://github.com/mono/SkiaSharp)
which will hopefully replace our Cairo backend for non-Windows platforms.
[Skia](https://skia.org/) is a modern 2D graphics API that better fits
Avalonia's drawing model than Cairo and also works on mobile platforms.

## AppBuilder

The `Application` was previously semi-platform-specific in that if you wanted to
have an application that ran on Desktop, Android and iOS you would need a
different `Application` class for each. This wasn't ideal because you'll often
want to share 90% of the `Application`.

Alpha 4 adds the concept of an `AppBuilder` which is called in your `Main`
entrypoint and will configure the platform-specific parts of the `Application`.
This means that you typical Avalonia program's entrypoint will now look
something like this for desktop applications:

```csharp
static void Main(string[] args)
{
    AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .Start<MainWindow>();
}
```

While iOS applications will be configured using something like:

```csharp
AppBuilder.Configure<App>()
    .UseiOS()
    .UseSkiaViewHost()
    .UseSkia()
    .Start<MainWindow>();
```

## Data Validation

[Jeremy Koritzinsky](https://github.com/jkoritzinsky) implemented support for
data validation on bindings. Currently exceptions and `INotifyDataErrorInfo` are
supported but we hope to extend support to `IDataErrorInfo` and
`System.ComponentModel.DataAnnotations` soon. To enable data validation, set
the binding's `EnableValidation` property to `true`:

```xml
<TextBox Text="{Binding Path=Value, EnableValidation=True}"/>
```

## Window Icons

[Jeremy Koritzinsky](https://github.com/jkoritzinsky) also implemented support
for window icons. The following example loads the icon for a window from a
[Manifest Resource](https://msdn.microsoft.com/en-us/library/wwtazz9d.aspx)
called `test_icon.ico`.

```xml
<Window Icon="resm:test_icon.ico"/>
```

## System Dialogs

[Nikita Tsukanov](https://github.com/kekekeks) and
[Dan Walmsley](https://github.com/danwalmsley)  implemented system Dialogs
for File Open, Choosing a Directory and File Save. An example:

```csharp
var dialog = new OpenFileDialog();
var fileNames = await dialog.ShowAsync();
```

## Designer Improvements

[Abdelkarim Sellamna](https://github.com/abdelkarim) started a nice clean
up of our designer UI, bringing a familiar look and feel:

![Designer](/blog/images/2016-08-06-avalonia-alpha4/designer.png)

In addition, [Nikita Tsukanov](https://github.com/kekekeks) implemented support
for editing XAML files in dlls and PCLs.

## XAML Behaviors

[Wiesław Šoltés](https://github.com/wieslawsoltes) has ported [UWP's behaviors
to Avalonia](https://github.com/XamlBehaviors/XamlBehaviors). XAML Behaviors are
an easy-to-use means of adding common and reusable interactivity to your
Avalonia applications with minimal code.

## Thanks for Reading!

Please file any bugs you find on [our issue tracker](https://github.com/AvaloniaUI/Avalonia/issues) and come join us in our
[Gitter room](https://gitter.im/AvaloniaUI/Avalonia).
