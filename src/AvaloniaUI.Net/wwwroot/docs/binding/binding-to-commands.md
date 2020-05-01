---
Title: Binding to Commands
Order: 35
---
Controls that carry out an action, such as [`Button`](/api/Avalonia.Controls/Button/D29AE9A9) have
a `Command` property which can be bound to an 
[`ICommand`](https://docs.microsoft.com/en-gb/dotnet/api/system.windows.input.icommand?view=netstandard-2.0).
When the control is activated (e.g. when a button is clicked) the `ICommand.Execute` method will
be called.

A good implementation of `ICommand` can be found in ReactiveUI's 
[`ReactiveCommand`](https://reactiveui.net/docs/handbook/commands/). If you've created your application
using the [Avalonia MVVM Application](/docs/quickstart/create-new-project) template then this will
be available by default. See the [ReactiveUI](https://reactiveui.net/docs/handbook/commands/)
documentation for more information.

An example:

```csharp
namespace Example
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            DoTheThing = ReactiveCommand.Create(RunTheThing);
        }

        public ReactiveCommand<Unit, Unit> DoTheThing { get; }

        void RunTheThing()
        {
            // Code for executing the command here.
        }
    }
}
```

```xml
<Window xmlns="https://github.com/avaloniaui">
    <Button Command="{Binding DoTheThing}">Do the thing!</Button>
</Window>
```

## CommandParameter

You can also pass a parameter to the command using the `CommandParameter` property:

```csharp
namespace Example
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            DoTheThing = ReactiveCommand.Create<string>(RunTheThing);
        }

        public ReactiveCommand<string, Unit> DoTheThing { get; }

        void RunTheThing(string parameter)
        {
            // Code for executing the command here.
        }
    }
}
```

```xml
<Window xmlns="https://github.com/avaloniaui">
    <Button Command="{Binding DoTheThing}" CommandParameter="Hello World">Do the thing!</Button>
</Window>
```

Note that no type conversion is carried out on `CommandParameter`, so if you need you type
parameter to be something other than `string` you must supply an object of that type in XAML.
For example to pass an `int` parameter you could use:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:sys="clr-namespace:System;assembly=mscorlib">
    <Button Command="{Binding DoTheThing}">
        <Button.CommandParameter>
            <sys:Int32>42</sys:Int32>
        </Button.CommandParameter>
        Do the thing!
    </Button>
</Window>
```

Like any other property, `CommandParameter` can also be bound.

## Binding To Methods

Sometimes you just want to call a method when a button is clicked without the ceremony of creating
a command. You can do that too!

```csharp
namespace Example
{
    public class MainWindowViewModel : ViewModelBase
    {
        public void RunTheThing(string parameter)
        {
            // Code for executing the command here.
        }
    }
}
```

```xml
<Window xmlns="https://github.com/avaloniaui">
  <Button Command="{Binding RunTheThing}" CommandParameter="Hello World">Do the thing!</Button>
</Window>
```
