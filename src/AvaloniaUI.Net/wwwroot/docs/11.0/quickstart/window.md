---
Title: Windows
Order: 30
---

`Window` is the top-level control in Avalonia.

A window consists of two parts: a [XAML file](/docs/quickstart/intro-to-xaml)
(e.g. `MainWindow.axaml`) and a [codebehind file](/docs/quickstart/codebehind) (e.g.
`MainWindow.axaml.cs`). The codebehind defines a .NET class which represents the window.

For more information and examples, see [the `Window` control](/docs/controls/window).

The default application templates create a single `Window` called `MainWindow`. You can
create additional windows from templates too:

## Visual Studio

1. Right click the folder in Solution Explorer that you'd like to add the window to 
2. Select the `Add -> New Item` menu item
3. In the dialog that appears, navigate to the "Avalonia" section in the category tree
4. Select "Window (Avalonia)"
5. Enter your window name under "Name"
6. Click the "Add" button

## .NET Core CLI

Run this command replacing `[namespace]` with the namespace you'd like to create the window in
and `[name]` with the name of the window.

```powershell
dotnet new avalonia.window -na [namespace] -n [name]
```

For more information see
[the .NET core templates repository](https://github.com/AvaloniaUI/avalonia-dotnet-templates/).
