---
Title: Logging Errors and Warnings
Order: 1000
---

Avalonia uses [Serilog](https://github.com/serilog/serilog) for logging via
the Avalonia.Logging.Serilog assembly.

The following method should be present in your Program.cs file:

```csharp
public static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .LogToDebug();
```

By default, this logging setup will write log messages with a severity of
`Warning` or higher to `System.Diagnostics.Debug`. The severity can be controlled
by passing a `level` parameter to `LogToDebug()`.

## Areas

Each Avalonia log message has an "Area" that can be used to filter the log to
include only the type of events that you are interested in. These are described
by the members of `Avalonia.Logging.LogArea` static class and are currently:

- `Property`
- `Binding`
- `Animations`
- `Visual`
- `Layout`
- `Control`

The `LogToDebug` method doesn't currently allow specifying an area to display,
so you need to set up the serilog pipeline manually and use a filter. This can
be done using the following code:

```csharp
SerilogLogger.Initialize(new LoggerConfiguration()
    .Filter.ByIncludingOnly(Matching.WithProperty("Area", LogArea.Layout))
    .MinimumLevel.Verbose()
    .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
    .CreateLogger());
```

## Removing Serilog

If you don't want a dependency on Serilog in your application, simply remove
the reference to Avalonia.Logging.Serilog and the code that initializes it. If
you do however still want some kinda of logging, there are two steps:

- Implement `Avalonia.Logging.ILogSink`
- Assign your implementation to `Logger.Sink`
