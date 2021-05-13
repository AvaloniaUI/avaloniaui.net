---
Title: Avalonia Beta 1
Published: 2018-02-18
Category: Release
Author: Steven Kirk
---

We’re pleased to announce that Beta 1 of [Avalonia](https://github.com/AvaloniaUI/Avalonia) is now available.

Avalonia is a cross platform .NET UI framework inspired by WPF, with XAML, data binding, lookless controls and much more. Avalonia is the only way to bring XAML-based applications to Windows, Mac and Linux.

We've decided that we're now at a stage where we're happy to come out of Alpha and call our current state Beta! People are already creating awesome applications with Avalonia, such as [Avalon Studio](https://github.com/VitalElement/AvalonStudio) - an Avalonia-based IDE for C# and C++ and [Core2D](https://github.com/wieslawsoltes/Core2D) - a data-driven 2D diagram editor, and this represents the first step towards getting the framework production-ready.

The easiest way to get started with Avalonia is to install our [Visual Studio plugin](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaforVisualStudio) and use the templates we provide there or use our [.NET core templates](https://github.com/AvaloniaUI/avalonia-dotnet-templates). Check out our [samples](https://github.com/AvaloniaUI/Avalonia/tree/master/samples) for some examples of how to get started writing an application.

<iframe width="560" height="315" src="https://www.youtube.com/embed/21LYHJok82s" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>

There have been a few architectural changes in this release that we hope will aid us moving forward:

- We are now on the .NET Standard 2.0 platform
- We're now using [Portable.Xaml](https://github.com/cwensley/Portable.Xaml) as our XAML parser
- We've removed support for Cairo and GTK2 - you should now use the Skia and GTK3 or MonoMac backends for non-windows platforms
- We're now using ReactiveUI 8 rather than shipping our own fork
- We've created the [avaloniaui.net](http://avaloniaui.net/) site which, although very bare-bones at the moment, we hope to grow into a decent source of documentation

So here's some of the major features in this release:

## Deferred Rendering

We were previously rendering the entire window each time something changed, which was obviously rather inefficient. [#827](https://github.com/AvaloniaUI/Avalonia/pull/827) added a `DeferredRenderer`  which renders to a low-level scenegraph on the UI thread which is then rendered to the window on a separate render thread.

This greatly improves our rendering performance - particularly when animations are involved - and also gives us proper hit-testing of controls.

The old renderer is still available as the `ImmediateRenderer` class for those folks who want to render everything every frame.

## Monomac-based Backend

[#1005](https://github.com/AvaloniaUI/Avalonia/pull/1005) introduced a new Monomac-based backend for Mac OSX platforms. 
The main issue with GTK2/GTK3 backends used previously was the huge set of binaries (~60MB) that needs to be shipped with your app. Another problem with those binaries is that it's complicated to package them into your app bundle.
Our new backend also makes use of Cocoa window dialogs, which makes your app to look more native.

## Relative Source Syntax Sugar

[#1209](https://github.com/AvaloniaUI/Avalonia/pull/1209) introduced shorthand for binding to other controls relative to the control being bound. In WPF, this is achieved by using the `RelativeSource` syntax on a `{Binding}`, as a (slightly contrived example) this is how you would bind a `TextBlock`'s text to the `Tag` property on a parent control:

```xml
<Border Tag="Hello World">
  <TextBlock Text="{Binding Tag, RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Border} AncestorLevel=1}}"/>
</Border>
```

Now Avalonia has new syntax to achieve this without the verbosity:

```xml
<Border Tag="Hello World">
  <TextBlock Text="{Binding $parent.Tag}"/>
</Border>
```

`$parent` isn't the only syntax this feature adds, here are some more things you can do:


| Shorthand                 | Implementation                           |
| ------------------------- | ---------------------------------------- |
| `$self`                   | `Mode = Self`                            |
| `$parent`                 | `Mode = FindAncestor; AncestorLevel = 1` |
| `$parent[Level]`          | `Mode = FindAncestor; AncestorLevel = Level +1` |
| `$parent[ns:Type]`        | `Mode = FindAncestor; AncestorType = ns:Type` |
| `$parent[ns:Type; Level]` | `Mode = FindAncestor; AncestorType = ns:Type; AncestorLevel = Level + 1` |

## Drawings

`Drawing` is a convenient way to create vector icons, used by WPF. The [Visual Studio Image Library](https://msdn.microsoft.com/en-us/library/windows/desktop/mt791579(v=vs.85).aspx) provides many icons as Drawings, and there are other tools that can produce them. They are much more lightweight compared to `Shape` controls since they are not part of the visual tree. 

[1117](https://github.com/AvaloniaUI/Avalonia/pull/1117) added support for Drawings to Avalonia, though our standard image control still can't display them - in the meantime while we add support for that, drawings can be displayed in a `DrawingPresenter` control.

## New Previewer with .NET Core support in Visual Studio extension

[#1105](https://github.com/AvaloniaUI/Avalonia/pull/1105) introduced a new previewer architecture which should allow us to make designers for non-windows platforms. The previous previewer used win32 API voodoo to reparent the window of the application into the designer. It also used some external executable that would initialize various things via reflection. The whole thing was tied to win32 and full .NET framework, it also made several assumptions about your app. The new previewer architecture instead uses a TCP transport protocol which communicates between the application and the designer in a platform-independent manner. It still has an option to embed the HWND which is used by our Visual Studio extension, but the default one will transfer rendered bitmap data via TCP. Speaking of which, the entire infrastructure for remote widgets is exposed in our public API via `RemoteServer` and `RemoteWidget` classes.

The [AvaloniaVS](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaforVisualStudio) extension has already been updated to use this new protocol, and hopefully now designers for other IDEs will be coming soon!

## StaticResource and DynamicResource

[#1130](https://github.com/AvaloniaUI/Avalonia/pull/1130) added `Control.Resources`, `StaticResource` and `DynamicResource`.

The new features are designed to follow WPF/UWP as closely as possible - previously, resources could only be contained in `<Style>`s and we used `{StyleResource}` to access these resources. Now each control has its own `Resources` dictionary and `{StaticResource}` and `{DynamicResource}` are used to access them. `{StyleResource}` has now be removed, so you will need to update all uses of that markup extension to `{DynamicResource}`.

## Bind Commands to Methods

Ever get annoyed that you have to create an `ICommand` to bind a button command, and wish you could just point your binding to a method? Since [`#1179`](https://github.com/AvaloniaUI/Avalonia/pull/1179) you can! 

```csharp
public class ViewModel
{
    public void ButtonClicked()
    {
        Console.WriteLine("Hooray!");
    }
}
```

```xml
<Button Command="{Binding ButtonClicked}"/>
```

## Calendar Control

[#1244](https://github.com/AvaloniaUI/Avalonia/pull/1244) ported the [Silverlight Calendar](https://github.com/MicrosoftArchive/SilverlightToolkit) control to Avalonia, for all your calendaring needs.

## WPF integration

We've added a seamless integration with WPF: you can embed Avalonia controls in your WPF application without creating extra native windows and with full layout integration which is allowed by the fact that WPF's and Avalonia's layout systems are almost the same. See the demo [here](https://www.youtube.com/watch?v=uFKcO3RxN7k).


## Various Other Features

Here's a curated list of the other interesting features that were introduced in this release. To see everything included in the Beta 1 release, see the [milestone](https://github.com/AvaloniaUI/Avalonia/milestone/2).

- `#894` Buttons are disabled when there is a null in the binding chain for Command
- `#1085` Added FindAncestor binding mode. 
- `#1086` Upgrade ReactiveUI to the v8 alpha
- `#1128` Add a IsPressed property to Button 
- `#1133` ToolTip: IsOpen, Placement, Offset
- `#1145` Switched to .NET Standard 2.0 
- `#1146` Added ShowTaskbarIcon implementation
- `#1150` Screen implementation
- `#1174` Removed GTK2 and Cairo support
- `#1175` Added Orientation and IsIndeterminate properties to ProgressBar
- `#1253` Add Vertical orientation option for PageSlide animations
- `#1265` Implemented three states on ToggleButton, CheckBox and RadioButton
- `#1273` Added AppBuilder methods for logging
- `#1079` Portablexaml


## Breaking changes in this release

We are also now tracking breaking changes at our [wiki page](https://github.com/AvaloniaUI/Avalonia/wiki/Breaking-Changes).

### `BuildAvaloniaApp` and previewer.

You now need `BuildAvaloniaApp` static method in the class with your entry point (typically in `Program.cs` or `App.xaml.cs`) which should be called from `Main`:

```csharp
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
.LogToDebug();

```

Previewer *won't* be able to work without it.

### `DataContextChanging` and `DataContextChanged`

They were replaced by `OnDataContextBeginUpdate` and `OnDataContextEndUpdate`


### `Static` and `Type` markup extensions

They were replaced by standard `x:Static` and `x:Type`, add `xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"` to the root of your XAML file.

### `StyleResource`

`StyleResource` has been replaced by `StaticResource` and `DynamicResource` as in other XAML frameworks. `StaticResource` and `DynamicResource` in Avalonia search both `Control.Resources` _and_ `Style.Resources`.

### Mouse device

`MouseDevice` is no longer available from the global context, you need to obtain one from `TopLevel` (`Window`, `Popup`, etc). Call `GetVisualRoot()` in your control and cast it to `IInputRoot`.

```csharp
var pos = (_control.GetVisualRoot() as IInputRoot)?.MouseDevice?.Position ?? default(Point);
```


## Getting started

Follow instructions [here](/guides/quickstart).


<a href="https://www.reddit.com/r/csharp/comments/7yl29q/avaloniaui_beta_1_released_a_crossplatform_xaml/"><img src="//www.redditstatic.com/spreddit5.gif"/>Discuss on Reddit</a>
