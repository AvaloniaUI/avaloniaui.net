---
Title: ContentControl
---
The [`ContentControl`](/api/Avalonia.Controls/ContentControl) displays data according to a
[data template](/docs/templates/datatemplate).

## Common Properties

|Property|Description|
|--------|-----------|
|`Content`|The content to display in the control|

## Source code

[ContentControl.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/ContentControl.cs)

## Display content

At its simplest, a `ContentControl` displays the data assigned to its
[`Content`](/api/Avalonia.Controls/ContentControl/4B02A756) property.

For example:

```xml
<ContentControl Content="Hello World!"/>
```

Will display the string "Hello World!". The `Content` property is the control's default property
and so the above example can also be written as:

```xml
<ContentControl>Hello World!</ContentControl>
```

If you assign a control to a `ContentControl` then it will display the control, for example:

```xml
<ContentControl>
  <Button>Click Me!</Button>
</ContentControl>
```

## Display content with templates

So far so uninteresting. Where `ContentControl` becomes useful is in tandem with 
[data binding](/docs/binding) and [data templates](/docs/templates/datatemplate). By setting the
[`ContentTemplate`](/api/Avalonia.Controls/ContentControl/ACED680E) property one can specify how
the data in the `Content` property is displayed. For example given the following view models:

```csharp
namespace Example
{
    public class MainWindowViewModel : ViewModelBase
    {
        object content = new Student("Jane", "Deer");

        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }
    }

    public class Student
    {
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}
```

> Note: The following examples assume an instance of `MainWindowVieModel` is assigned to the Window's
  `DataContext`. See [the section on `DataContext`](/docs/binding/datacontext) for more information.

We can display the student's first and last name in a `ContentControl` using the `ContentTemplate`
property:

```xml
<Window xmlns="https://github.com/avaloniaui">
  <ContentControl Content="{Binding Content}">
    <ContentControl.ContentTemplate>
      <DataTemplate>
        <Grid ColumnDefinitions="Auto,Auto" RowDefinitions="Auto,Auto">
          <TextBlock Grid.Row="0" Grid.Column="0">First Name:</TextBlock>
          <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding FirstName}"/>
          <TextBlock Grid.Row="1" Grid.Column="0">Last Name:</TextBlock>
          <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding LastName}"/>
        </Grid>
      </DataTemplate>
    </ContentControl.ContentTemplate>
  </ContentControl>
</Window>
```
![Student first and last name](images/student-first-last-name.png)

For more information see the [data templates](/docs/templates/datatemplate) section.
