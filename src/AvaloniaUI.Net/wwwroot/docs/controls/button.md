---
Title: Button
---
The `Button` control is a [`ContentControl`](contentcontrol) which reacts to pointer presses.

A button notifies clicks by raising the [`Click`](/api/Avalonia.Controls/Button/61B1E7A8) event.
A click is distinct from a `PointerDown` event in that it is raised by default when the button is
pressed and then released (although this behavior can be changed by setting the 
[`ClickMode`](/api/Avalonia.Controls/Button/7B4CADF5) property).

Alternatively an instance of [`ICommand`](https://docs.microsoft.com/en-gb/dotnet/api/system.windows.input.icommand?view=netstandard-2.0)
can be assigned or bound to the button's [`Command`](/api/Avalonia.Controls/Button/4AAA993D)
property. This command will be executed when the button is clicked. For more information see
[binding to commands](/docs/binding/binding-to-commands).

[`Click`](/api/Avalonia.Controls/Button/61B1E7A8) is just one of several events that Buttons have.  Another example is [`PointerEnter`](/api/Avalonia.Input/InputElement/B4FED8A5) or [`PointerLeave`](/api/Avalonia.Input/InputElement/0ABE1887).  You can get the full list of Button Events [here](/api/Avalonia.Controls/Button/#Events).

The Button control's full documentation can be found [here](/api/Avalonia.Controls/Button){target="_blank"}

## Common Properties

|Property|Description|
|--------|-----------|
|`ClickMode`|Describes how the button should react to clicks|
|`Command`|A command to be invoked when the button is clicked|
|`CommandParameter`|A parameter to be passed to `Command`|
|`Content`|The content to display in the button|
|`IsDefault`|When set, pressing the enter key clicks the button even if not focused|
|`IsPressed`|Set when the button is depressed|

## Pseudoclasses

|Pseudoclass|Description|
|-----------|-----------|
|`:pressed`|Set when the button is depressed|

## Source code
[Button.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Button.cs)

## Examples

### Basic button
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<StackPanel>
		<Button Width="160" >My Button</Button>
	</StackPanel>
</Window>
```
produces following output with **Windows 10**  
![Basic button](images/button_basic.png)

### Colored button
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<StackPanel>
		<Button Width="160" Foreground="White" Background="Red">White text, red background</Button>
	</StackPanel>
</Window>
```
produces following output with **Windows 10**  
![Basic button](images/button_colors.png)

### Binding to a View Model Command
 It is possible to bind a view model command to a simple method or with a ReactiveCommand. There are lots of advantages to the ReactiveCommand binding for all but the simplest user interfaces such as being able to pass an `IObservable<bool>` object in to have it dynamically calculate state.  Both methods are displayed below.  First the "simple" method binding:
 
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
    <StackPanel>
        <Button Width="160" Command="{Binding OnClickCommand}">My Button</Button>
    </StackPanel>
</Window>
```

The Code in the bound View Model for the above will either look like:
```cs
public void OnClickCommand()
{
	// do something
}
```

If using ReactiveCommands:
```cs
public MainWindowViewModel()
{
	OnClickCommand = ReactiveCommand.Create(() => { /* do something */ });
}

public ReactiveCommand OnClickCommand { get; }
```

### Binding to Events
 
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
    <StackPanel>
        <Button Width="160" 
                Click="OnButtonClick"
                PointerEnter="OnPointerEnter"
                Content="My Button"/>
    </StackPanel>
</Window>
```
The corresponding C# code in the View's cs file:
```cs
private void OnButtonClick(object sender, RoutedEventArgs e)
{
	//do something on click
}

private void OnPointerEnter(object sender, PointerEventArgs e)
{
	//do something when pointer enters
}
```



