---
Title: Routing
Order: 20
---
[ReactiveUI routing](https://reactiveui.net/docs/handbook/routing/) consists of an [IScreen](https://reactiveui.net/api/reactiveui/iscreen/) that contains current [RoutingState](https://reactiveui.net/api/reactiveui/routingstate/), several [IRoutableViewModel](https://reactiveui.net/api/reactiveui/iroutableviewmodel/)s, and a platform-specific XAML control called [RoutedViewHost](https://github.com/AvaloniaUI/Avalonia/blob/55458cf7af24d6c987268ab5ff8a1ead1173310b/src/Avalonia.ReactiveUI/RoutedViewHost.cs). `RoutingState` manages the view model navigation stack and allows view models to navigate to other view models. `IScreen` is the root of a navigation stack; despite the name, its views don't have to occupy the whole screen. `RoutedViewHost` monitors an instance of `RoutingState`, responding to any changes in the navigation stack by creating and embedding the appropriate view.

## Routing Example

Create a new empty project from Avalonia templates. To use those, clone the [avalonia-dotnet-templates](https://github.com/AvaloniaUI/avalonia-dotnet-templates) repository, install the templates and create a new project named `RoutingExample` based on `avalonia.app` template. Install `Avalonia.ReactiveUI` package into the project.

```sh
git clone https://github.com/AvaloniaUI/avalonia-dotnet-templates
dotnet new --install ./avalonia-dotnet-templates
dotnet new avalonia.app -o RoutingExample
cd ./RoutingExample
dotnet add package Avalonia.ReactiveUI
```

**FirstViewModel.cs**

First, create routable view models and corresponding views. We derive routable view models from the `IRoutableViewModel` interface from `ReactiveUI` namespace, and from `ReactiveObject` as well. `ReactiveObject` is the base class for [view model classes](https://reactiveui.net/docs/handbook/view-models/), and it implements `INotifyPropertyChanged`.

```cs
namespace RoutingExample
{
    public class FirstViewModel : ReactiveObject, IRoutableViewModel
    {
        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public FirstViewModel(IScreen screen) => HostScreen = screen;
    }
}
```

**FirstView.xaml**

```xml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="RoutingExample.FirstView">
    <StackPanel HorizontalAlignment="Center"
                VerticalAlignment="Center">
        <TextBlock Text="Hi, I'm the first view!" />
        <TextBlock Text="{Binding UrlPathSegment}" />
    </StackPanel>
</UserControl>
```

**FirstView.xaml.cs**

If we need to handle view model activation and deactivation, then we add a call to WhenActivated to the view. Generally, a rule of thumb is to always add WhenActivated to your views, see [Activation](/docs/reactiveui/activation) docs for more info.

```cs
namespace RoutingExample
{
    public class FirstView : ReactiveUserControl<FirstViewModel>
    {
        public FirstView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
```

**MainWindowViewModel.cs**

Then, create a view model implementing the `IScreen` interface. It contains current `RoutingState` that manages the navigation stack. `RoutingState` also contains helper commands allowing you to navigate back and forward.

:::note
Actually, you can use as many `IScreen`s as you need in your application. Despite the name, it doesn't have to occupy the whole screen. You can use nested routing, place `IScreen`s side-by-side, etc.
:::

```cs
namespace RoutingExample
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();
            
        // The command that navigates a user to first view model.
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public MainWindowViewModel()
        {
            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            //
            GoNext = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new FirstViewModel(this))
            );
        }
    }
}
```

**MainWindow.xaml**

Now we need to place the `RoutedViewHost` XAML control to our main view. It will resolve and embedd appropriate views for the view models. Note, that you need to import `rxui` namespace for `RoutedViewHost` to work. Additionally, you can override animations that are played when `RoutedViewHost` changes a view — simply override `RoutedViewHost.FadeInAnimation` and `RoutedViewHost.FadeOutAnimation` properties in XAML.

:::note
For latest builds from MyGet use `xmlns:rxui="http://reactiveui.net"`, for 0.8.0 release on NuGet use `xmlns:rxui="clr-namespace:Avalonia;assembly=Avalonia.ReactiveUI"` as in the example below.
:::

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:rxui="clr-namespace:Avalonia;assembly=Avalonia.ReactiveUI"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="RoutingExample.MainWindow"
        Title="RoutingExample">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        <rxui:RoutedViewHost Grid.Row="0" Router="{Binding Router}">
            <rxui:RoutedViewHost.DefaultContent>
                <TextBlock Text="Default content"
                           HorizontalAlignment="Center"
                           VerticalAlignment="Center" />
            </rxui:RoutedViewHost.DefaultContent>
        </rxui:RoutedViewHost>
        <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="15">
            <StackPanel.Styles>
                <Style Selector="StackPanel > :is(Control)">
                    <Setter Property="Margin" Value="2"/>
                </Style>
                <Style Selector="StackPanel > TextBlock">
                    <Setter Property="VerticalAlignment" Value="Center"/>
                </Style>
            </StackPanel.Styles>
            <Button Content="Go next" Command="{Binding GoNext}" />
            <Button Content="Go back" Command="{Binding GoBack}" />
            <TextBlock Text="{Binding Router.NavigationStack.Count}" />
        </StackPanel>
    </Grid>
</Window>
```

**MainWindow.xaml.cs**

Here is the code-behind for main view declared above.

```cs
namespace RoutingExample
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
```

Finally, add `.UseReactiveUI()` to your `AppBuilder` and initialize `DataContext`.

```cs
namespace RoutingExample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainWindow>(
                () => new MainWindowViewModel()
            );
        }
    
        public static AppBuilder BuildAvaloniaApp()
        {
            // Router uses Splat.Locator to resolve views for
            // view models, so we need to register our views.
            //
            Locator.CurrentMutable.Register(() => new FirstView(), typeof(IViewFor<FirstViewModel>));
                    
            return AppBuilder
                .Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToDebug();
        }
    }
}
```

:::note
Instead of registering views manually, you can use custom `IViewLocator` implementation, or `Locator.RegisterViewsForViewModels` method which registers all `IViewFor` implementations from an assembly. See [View Location](https://reactiveui.net/docs/handbook/view-location/) for details.
:::

Now, you can run the app and see routing in action!

```sh
dotnet run --framework netcoreapp2.1
```

<img src="/images/routing-kde.gif">
