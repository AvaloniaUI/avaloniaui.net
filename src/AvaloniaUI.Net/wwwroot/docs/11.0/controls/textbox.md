---
Title: TextBox
---
The `TextBox` control is an editable text field where a user can input text.

## Reference
[TextBox](http://reference.avaloniaui.net/api/Avalonia.Controls/TextBox/)

## Source code
[TextBox.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/TextBox.cs)

## Examples

### Basic one line TextBox

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<StackPanel Margin="10">
		<TextBox />
	</StackPanel>
</Window>
```
produces the following output in **Windows 10**  
![Basic TextBox](images/textbox_basic.png)

### Password input TextBox

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<StackPanel Margin="10">
		<TextBox PasswordChar="#" />
	</StackPanel>
</Window>
```
produces the following output in **Windows 10** when text is input  
![Password TextBox](images/textbox_password.png)

### TextBox with watermark

Avalonia can show a "watermark" in a `TextBox`, which is a piece of text that is displayed when the `TextBox` is empty (in HTML5 this is called a *placeholder*)

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<StackPanel Margin="10">
		<TextBox Watermark="Street address" />
	</StackPanel>
</Window>
```
produces the following output in **Windows 10**   
![Watermark TextBox](images/textbox_watermark.png)

### Multiline TextBox

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="AvaloniaAppTemplate">
	<Grid Margin="10">
		<TextBox AcceptsReturn="True" TextWrapping="Wrap" />
	</Grid>
</Window>
```
produces the following output in **Windows 10** when text is input  
![Multiline TextBox](images/textbox_multiline.png)
