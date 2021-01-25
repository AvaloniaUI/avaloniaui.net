---
Title: Code-behind
Order: 25
---

[Window](/docs/window) and [UserControl](/docs/usercontrol) files also have an associated
_code-behind_ file which usually has the extension `.xaml.cs` or `.axaml.cs` and may be displayed collapsed under
the XAML file in your editor. Below you can see a `MainWindow.xaml` file along with its markdown
file `MainWindow.xaml.cs` in Visual Studio:

![Code-behind in Visual Studio](images/codebehind-vs.png)

The code-behind file by default defines a .NET class with the same name as your XAML file, e.g.

```csharp
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

```

Note that this class definition corresponds closely to the XAML file:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow">
</Window>
```

- The base class, `Window` is the root element of the XAML file
- The `x:Class` attribute references the fully-qualified name of the class defined in code-behind

:::note
If you make any changes to the base class, namespace, or name of the class, make sure to update both
the code-behind and the XAML to ensure they match.
:::

In addition, the class contains two more things of interest:

- It enables [DevTools](/docs/quickstart/devtools) in debug mode
- It defines an `InitializeComponent` method which is used to load the XAML at runtime

## Locating Controls

One of the main uses of the code-behind file is to manipulate controls using C# code. To do this
you'll usually first want to get a reference to a named control. This can be done using the
`FindControl<T>` method:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication4.MainWindow">
  <Button Name="myButton">Hello World</Button>
</Window>
```

```csharp
public class MainWindow : Window
{
    private Button myButton;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        myButton = this.FindControl<Button>("myButton");
    }
}
```

`FindControl` must be called after `AvaloniaXamlLoader.Load` has run because it's not until this
point that the control will have been created.

:::note
This step is not necessary in WPF and UWP because they have a code-generation step which creates
this code for you. Avalonia does not currently have such a step so you'll need to do it manually.
:::

## Handling Events

Another common use for the code-behind file is to define _event handlers_. Event handlers are
defined as methods in the code-behind and referenced from XAML. For example to add a handler for
a button click:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication4.MainWindow">
  <Button Click="MyButton_Click">Hello World</Button>
</Window>
```

```csharp
public class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void MyButton_Click(object sender, RoutedEventArgs e)
    {
        // Handle click here.
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
```
