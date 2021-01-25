---
Title: Compiled Bindings
Order: 20
---

Avalonia 0.10 includes experimental support for compiled bindings. When using compiled bindings, binding paths are verified at compile time and do not use reflection at runtime.

To enable compiled bindings, firstly you must add an `x:DataType` attribute to one of your controls - often this will be done on the root element in a XAML file. The following example sets the data type for a `Window` to the `AvaloniaApplication.ViewModels.MainWindowViewModel` type:

```
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
        xmlns:vm="using:AvaloniaApplication.ViewModels" 
        x:Class="BindingDemo.MainWindow"
        x:DataType="vm:MainWindowViewModel">
```

Once you've set the `x:DataType` there are two ways to use compiled bindings:

## Use the CompiledBinding markup extension

Replacing usages of `{Binding}` with `{CompiledBinding}` will switch individual bindings to use compiled bindings:

```
<TextBox Text="{CompiledBinding Value}"/>
```

## Set x:CompileBindings

Setting `x:CompileBindings="True"` on the root element in a XAML file will force all uses of the `{Binding}` markup extension in the XAML file to use compiled bindings:

```
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
        xmlns:vm="using:AvaloniaApplication.ViewModels" 
        x:Class="BindingDemo.MainWindow"
        x:CompileBindings="True"
        x:DataType="vm:MainWindowViewModel">
  <TextBox Text="{Binding Value}"/>
</Window>
```

You can opt-out of compiled bindings in this case by using the `{ReflectionBinding}` markup extension.
