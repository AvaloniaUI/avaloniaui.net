---
Title: ReactiveUI
Order: 70
---
[ReactiveUI](https://reactiveui.net) is an advanced, composable, functional reactive model-view-viewmodel framework for all .NET platforms that is inspired by functional reactive programming. Avalonia ships its own fork of [ReactiveUI](https://reactiveui.net) in the `Avalonia.ReactiveUI` NuGet package. To use ReactiveUI and the the [MVVM Pattern](https://msdn.microsoft.com/en-us/library/hh848246.aspx) in your Avalonia solutions, add the package to your project via NuGet package manager GUI or execute the following command:

```sh
dotnet add package Avalonia.ReactiveUI
```

`Avalonia.ReactiveUI` includes Avalonia-specific helpers to handle [view model-based routing](https://reactiveui.net/docs/handbook/routing), [view activation](https://reactiveui.net/docs/handbook/when-activated/) and [scheduling](https://reactiveui.net/docs/handbook/scheduling/). Be sure to add `.UseReactiveUI()` to your `AppBuilder` before you use any of ReactiveUI's APIs in your application.

```csharp
public static class Program
{
    public static void Main(string[] args) => BuildAvaloniaApp().Start<MainWindow>();

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder
            .Configure<App>()
            // Add the line below to enable
            // ReactiveUI support in your app.
            .UseReactiveUI()
            .UsePlatformDetect()
            .LogToDebug();
    }
}
```

To get started with ReactiveUI, see the [Getting Started](https://reactiveui.net/docs/getting-started/) guide and the [tutorial](/docs/tutorial/). See also the [ReactiveUI Handbook](https://reactiveui.net/docs/handbook/) which describes advanced ReactiveUI features allowing you to build complex and scalable applications with ReactiveUI. See the pages below to learn how to handle view activation and how to use routing in your Avalonia applications.
