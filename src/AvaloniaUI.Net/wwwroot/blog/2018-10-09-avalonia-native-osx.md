---
Title: A new MacOS backend for the next version of AvaloniaUI
Published: 2018-10-09
Category: Release
Author: Dan Walmsley
---

Many of you who have been using Avalonia cross platform will have realised that there were issues with the Mac OSX backend.

For the past few months we have been working on a new backend for OSX. We now have a stable solution for OSX going forward.

Currently we are publishing the new backend as a seperate nuget package. The OSX backend now supports OpenGL GPU acceleration.

## Try the new Mac Backend today

To try the new backend today you need to update your application to Avalonia version `0.6.2-build6362-beta` for information on how to use Avalonia nightly builds see: [Using nightly build feed](https://github.com/AvaloniaUI/Avalonia/wiki/Using-nightly-build-feed)

Once you are on the nightly build you can the add the nuget package `Avalonia.Native` the latest build at time of writing is
`0.7.0-build42018101111`

You can add this to your csproj:

```xml
<PackageReference Include="Avalonia.Native" Version="0.7.0-build42018101111"/>
```

Then you will need to change your AppBuilder code usually found in `Program.cs` or `App.xaml.cs` to configure the use of `Avalonia.Native` when your application is run on OSX.

I recommend doing this by replacing the BuildAvaloniaApp method like so:

```csharp
        public static AppBuilder BuildAvaloniaApp()
        {
            var result = AppBuilder.Configure<App>();
            
            if(Platform.PlatformIdentifier == Platforms.PlatformID.MacOSX)
            {
                result.UseAvaloniaNative().UseSkia();
            }
            else
            {
                result.UsePlatformDetect();
            }

            return result;
        }


```

## What is Avalonia.Native?

Avalonia.Native isn't just an Avalonia backend for OSX, it is actually set of COM based interfaces that Avalonia can attach to. This allows Avalonia  backends to be written in native code on platforms where this is difficult in managed code.

You can find the repo for Avalonia.Native here:
https://github.com/AvaloniaUI/Avalonia.Native

In the near future Avalonia.Native will become the default backend for Mac so you wont need to take any steps to use it in your app.

Many thanks to [@kekekeks](https://github.com/kekekeks) who has helped to build this. You can also find me on Github or Gitter at [@danwalmsley](https://github.com/danwalmsley) if you require any assistance or encounter any issues.

![Screenshot](https://files.gitter.im/VitalElement/AvalonStudio/51zL/Screen-Shot-2018-10-12-at-00.47.42.png)

