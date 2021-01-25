---
Title: CheckBox
---
The `CheckBox` control is a [`ContentControl`](contentcontrol) which allows user to check an option. It is usually used to display a boolean option where selection is either *checked* or *unchecked*. But it also supports three state mode where selection is either *checked*, *indeterminate* or *unchecked*.

## Common Properties

|Property|Description|
|--------|-----------|
|`IsChecked`|Is `CheckBox` checked|
|`IsThreeState`|Is `CheckBox` in three state mode|

## Reference
[Checkbox](http://reference.avaloniaui.net/api/Avalonia.Controls/Checkbox/)

## Source code

[CheckBox.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/CheckBox.cs)

## Examples

### Basic checkbox

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="CheckBox sample">
    <StackPanel>
        <CheckBox>Not checked by default</CheckBox>
        <CheckBox IsChecked="True">Checked by default</CheckBox>
    </StackPanel>
</Window>
```
produces following output with **Windows 10**  
![Basic checkbox](images/checkbox_basic.png)

### Three state checkbox

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaAppTemplate.MainWindow"
        Title="CheckBox sample">
    <StackPanel>
        <CheckBox IsThreeState="True" IsChecked="False">Not checked by default</CheckBox>
        <CheckBox IsThreeState="True" IsChecked="True">Checked by default</CheckBox>
        <CheckBox IsThreeState="True" IsChecked="{x:Null}">Indeterminate by default</CheckBox>
    </StackPanel>
</Window>
```
produces following output with **Windows 10**  
![Three state checkbox](images/checkbox_threestate.png)
