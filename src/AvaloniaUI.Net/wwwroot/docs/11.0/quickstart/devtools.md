---
Title: Developer Tools
Order: 60
---

Avalonia has an inbuilt DevTools window which is enabled by calling the attached `AttachDevTools()`
method in a `Window` constructor. The default templates have this enabled when the program is
compiled in `DEBUG` mode:

```csharp
public class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
```

To open the DevTools, press F12, or pass a different `Gesture` to the `this.AttachDevTools()`
method.

![Avalonia DevTools](images/devtools.png)

:::note
There is a known issue when running under .NET core 2.1 that pressing F12 will cause the program to
quit. In this case, either switch to .NET core 2.0 or 3.0+ or change the open gesture to something
different, such as `Ctrl+F12`.
:::

## Logical and Visual Trees

The `Logical Tree` and `Visual Tree` tabs display the controls in the window's [logical and visual trees](/docs/advanced/trees). Selecting a control will show the properties of that control in the right-hand pane where they can be edited.

## Events

The events tab can be used to track the propogation of [events](/docs/input/events). Select the events to track in the left pane, and all events of that type will be shown in the center upper pane. Select one of these events to see the event route.

## Console

The console can be shown using the "View" -> "Console" menu. The console implements a C# REPL which can be used to run arbitrary C# code. In a console session, the following predefined variables are available:

- `help`: Displays a help message
- `e`: The control currently selected in the logical or visual tree
- `root`: The root of the visual tree
