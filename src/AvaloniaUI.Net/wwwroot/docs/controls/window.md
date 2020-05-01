---
Title: Window
---

[`Window`](/api/Avalonia.Controls/Window) is a top-level [`ContentControl`](contentcontrol).

You will not usually create instances of the `Window` class directly; instead the `Window` class
is usually sub-classed for each type of window to be shown by an application. For information on
how to create new window classes from templates see the [quickstart](/docs/quickstart/window).

## Common Properties

|Property|Description|
|--------|-----------|
|`Title`|The window title|
|`Icon`|The window icon|
|`SizeToContent`|Describes the window's auto-sizing behavior|
|`WindowState`|The minimized/maximized state of the window|

## Source code
[Window.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Window.cs)

## The main window

The main window is the window passed to `Application.Run` in the `AppMain` method of your your
`Program.cs` file:

```csharp
private static void AppMain(Application app, string[] args)
{
    app.Run(new MainWindow());
}
```

It can be retrieved at any time using the `Application.Current.MainWindow` property.

## Show, hide and close a window

You can show a window using the `Show` method:

```csharp
var window = new MyWindow();
window.Show();
```

Windows can be closed using the `Close` method. This has the same effect as when a user clicks the
window's close button:

```csharp
window.Close();

// A closed window cannot be shown.
window.Show();
```

Note that once a window has been closed, it cannot be shown again. If you want to re-show the
window then you should use the `Hide` method:

```csharp
window.Hide();

// Window can now be shown again later
window.Show();
```

See also [Prevent a window from closing](#prevent-a-window-from-closing)

## Show a window as a dialog

You can show a window as a modal dialog by calling the `ShowDialog` method. `ShowDialog` requires
an owner window to be passed:

```csharp
var window = new MyWindow();
window.ShowDialog(Application.Current.MainWindow);
```

The `ShowDialog` method will return immediately. If you want to wait for the dialog to be closed,
you can `await` the call:

```csharp
var window = new MyWindow();
await window.ShowDialog(Application.Current.MainWindow);
```

Dialogs can return a result by calling the `Close` method with an object. This result can then be
read by the caller of `ShowDialog`. For example:

```csharp
public class MyDialog : Window
{
    public MyDialog()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
        Close("OK Clicked!");
    }
}
```

```csharp
var dialog = new MyDialog();

// The result is a string so call `ShowDialog<string>`.
var result = await dialog.ShowDialog<string>(Application.Current.MainWindow);
```

## Prevent a window from closing

A window can be prevented from closing by handling the `Closing` event and setting `e.Cancel = true`:

```csharp
window.Closing += (s, e) =>
{
    e.Cancel = true;
};
```

You could also hide the window instead. This allows the window to be re-shown after the user clicks
the close button:

```csharp
window.Closing += (s, e) =>
{
    ((Window)s).Hide();
    e.Cancel = true;
};
```
