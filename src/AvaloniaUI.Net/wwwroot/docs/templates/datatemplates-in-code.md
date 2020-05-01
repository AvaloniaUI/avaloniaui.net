---
Title: Creating Data Templates in Code
Order: 10
---
Avalonia also supports creating data templates in code with the
[`FuncDataTemplate<T>`](/api/Avalonia.Controls.Templates/FuncDataTemplate_1/) class.

At its simplest you can create a data template by passing a lambda which accepts an instance
to the `FuncDataTemplate<T>` constructor:

```csharp
var template = new FuncDataTemplate<Student>(x =>
    new TextBlock
    {
        [!TextBlock.TextProperty] = new Binding("FirstName"),
    });
```

Is equivalent to:

```xml
<DataTemplate DataType="{x:Type local:Student}">
    <TextBlock Text="{Binding FirstName}"/>
</DataTemplate>
```
