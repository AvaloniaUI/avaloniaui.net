---
Title: Low-Level Graphics Options
---

## Enabling/Disabling GPU Rendering

To control whether rendering will be done by the GPU or in software, the following options can be
set in the AppBuilder.

### Windows

```csharp
AppBuilder.Configure<App>()
    .UsePlatformDetect()
    .With(new Win32PlatformOptions
    {
        // Setting to false disables GPU rendering
        AllowEglInitialization = false,
    })
    .StartWithClassicDesktopLifetime(args);
```

### OSX

```csharp
AppBuilder.Configure<App>()
    .UsePlatformDetect()
    .With(new AvaloniaNativePlatformOptions
    {
        UseGpu = false,
    })
    .StartWithClassicDesktopLifetime(args);
```

### Linux/X11

```csharp
AppBuilder.Configure<App>()
    .UsePlatformDetect()
    .With(new X11PlatformOptions
    {
        UseGpu = false,
    })
    .StartWithClassicDesktopLifetime(args);
```

GPU rendering will be enabled by default on Avalonia 0.10.0 and onwards.

## Controlling ANGLE on Windows

To control the platform used by ANGLE to render on Windows, use:

```csharp
.With(new AngleOptions()
{
    AllowedPlatformApis = new List<AngleOptions.PlatformApi>
    {
        AngleOptions.PlatformApi.DirectX11
    }
})
```
