---
Title: Avalonia Alpha 5
Published: 2017-05-08
Category: Release
Author: Steven Kirk
---


![Screenshot](https://hsto.org/web/54e/a14/77a/54ea1477ade448b796faed08780eb972.png)

We’re pleased to announce that alpha 5 of [Avalonia](https://github.com/AvaloniaUI/Avalonia) is now available.

Avalonia is a cross platform .NET UI framework inspired by WPF, with XAML, data binding, lookless controls and much more. Avalonia is the only way to bring XAML-based applications to Windows, Mac and Linux. We also have experimental mobile support!

You can get started by using the [Visual Studio plugin](https://visualstudiogallery.msdn.microsoft.com/b2203c92-2110-415b-b935-fcc01cf354f8?) which contains templates for creating Avalonia applications and a (basic) designer, and you can browse our [NuGet packages](https://www.nuget.org/packages?q=Tags%3A%22Avalonia%22). Please note that due to the clusterf#$k that are MS accounts we've had to re-upload the extension with a new identity (yet again), so if you have the old version installed you will have to manually uninstall and reinstall the new version.

Here are the new features in Alpha 5:

## .Net Core Support

Support for .NET core has landed! There are now two sets of desktop application templates in our extension, .NET Framework and .NET Core (we hope the merge these in the future) and we've started working on templates for `dotnet` which you can find [here](https://github.com/AvaloniaUI/avalonia-dotnet-templates). Many of our libraries now target .NET standard, and more will do so in future.

## GTK3 backend

We are now using P/Invoke based GTK3 backend instead of GTK#. Two main issues with GTK# are incompatibility with .NET Core and the need of additional native libraries that have to be built for each Linux distro. As a bonus point we now have a proper support for smooth scrolling on linix/osx.

On OSX it's now needed to install `gtk+3` package from `brew`. We are planning to create a native OSX backend, since with .NET Core 2.0 it's now possible to [run MonoMac-based applications on .NET Core](https://www.youtube.com/watch?v=edgosMqgcBc)


## Updated Mobile Platform Integration

We no longer pretend that desktop-like Window is a thing on mobile platforms (previously we had a singleton WindowImpl that mostly consisted of stubs). Instead we now provide an `AvaloniaView` class that derives from a native view and has `Content` property which can hold Avalonia controls. You can use this view in any way you want. We also provide `AvaloniaActivity` for Android and `AvaloniaWindow` (`UIWindow` with predefined associated `RootViewController`) for iOS for convenience purposes, both of which also have Content property.

Mobile integration now looks something like this:

```csharp
public override bool FinishedLaunching(UIApplication uiapp, NSDictionary options)
{
	AppBuilder.Configure<App>()
		.UseiOS()
		.UseSkia().SetupWithoutStarting();
	Window = new AvaloniaWindow() {Content = new YOUR_CONTROL_HERE(){DataContext = ...}};
	Window.MakeKeyAndVisible();
	return true;
}
```
```csharp
public class MainActivity : AvaloniaActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        if (Avalonia.Application.Current == null)           
        {
            AppBuilder.Configure(new App())
                .UseAndroid()
                .SetupWithoutStarting();
        }
        Content = YOUR_CONTROL_HERE(){DataContext = ...};
        base.OnCreate(savedInstanceState);
    }
}
```
## Linux FrameBuffer Support

We now have initial support for running Avalonia directly over a [Linux framebuffer](https://github.com/AvaloniaUI/Avalonia/tree/master/src/Linux/Avalonia.LinuxFramebuffer).

## Embedding Avalonia in WPF, WinForms and GTK

[@kekekeks](https://github.com/kekekeks) implemented embedding Avalonia in in WPF, WinForms and GTK applications. To see some examples of embedding Avalonia controls, check out the [the samples]().

## Extensibility system

[@jkoritzinsky](https://github.com/jkoritzinsky) implemented an extensibility system for automatically initializing 3rd party libraries and for automatically loading the best windowing and rendering subsystems available for the current OS platform. This will allow for control creators to automatically register services and themes required by their controls, as well as allowing dynamic addition of new renderers and windowing systems.

Take a look at the [pull request](https://github.com/AvaloniaUI/Avalonia/pull/707) for more details.

## Stream Binding Operator

Previously, when then binding system encountered an `IObservable<>` or a `Task` it automatically tried to subscribe to the observable or task and use the produced value as its value. However this [caused problems](https://github.com/AvaloniaUI/Avalonia/issues/711) when a user wanted to  actually bind to a property on a class that implements `IObservable`.

To allow both scenarios, we [added a '^' stream binding operator](https://github.com/AvaloniaUI/Avalonia/pull/764) which informs the binding system that the observable/task's result should be subscribed to. This can be used as follows:

```
<!-- Bind to the values produced by Content.Observable -->
<TextBlock Text="{Binding Content.Observable^}"/>

<!-- Bind to the Name property on the values produced by Content.Observable -->
<TextBlock Text="{Binding Content.Observable^.Name}"/>
```

The stream binding operator is [pluggable](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Markup/Avalonia.Markup/Data/ExpressionObserver.cs#L47) which should allow for some interesting possibilities!

## Bug Fixes

There have been many bugfixes by many contributors in this release - thanks to [@danwalmsley](https://github.com/danwalmsley), [@jkoritzinsky](https://github.com/jkoritzinsky), [@kekekeks](https://github.com/kekekeks), [@mikel785](https://github.com/mikel785), [@OronDF343](https://github.com/OronDF343), [@robbieknuth](https://github.com/robbieknuth), [@tfreitasleal](https://github.com/tfreitasleal), [@wieslawsoltes](https://github.com/wieslawsoltes) and [@yusuf-gunaydin](https://github.com/yusuf-gunaydin) for your PRs! You can see the list of bugs/enhancements for this release in the [release milestone](https://github.com/AvaloniaUI/Avalonia/milestone/7?closed=1)

## Thanks for Reading!

Please file any bugs you find on [our issue tracker](https://github.com/AvaloniaUI/Avalonia/issues) and come join us in our
[Gitter room](https://gitter.im/AvaloniaUI/Avalonia).

We're always looking for contributors - if you think you can help, please let us know in our gitter room!
