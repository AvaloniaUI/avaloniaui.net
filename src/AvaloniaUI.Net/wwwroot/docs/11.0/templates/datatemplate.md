---
Title: Data Templates
Order: 0
---
Many controls have a `Content` property, such as
[`ContentControl.Content`](/api/Avalonia.Controls/ContentControl/4B02A756). `Window` inherits from
[`ContentControl`](/docs/controls/contentcontrol), so lets use that as an example. You're probably
familiar with what happens when you put a control in the `Window.Content` property - the window
displays the control:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
  <Button HorizontalAlignment="Center"
          VerticalAlignment="Center">
    Hello World!
  </Button>
</Window>
```

![Hello World button](images/hello-world-button.png)

Similarly if you put a string as the window content, the window will display the string:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
  Hello World!
</Window>
```

![Hello World string](images/hello-world-string.png)

But what happens if you try to display an object as the window content?

```csharp
namespace Example
{
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

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:Example">
  <local:Student FirstName="Jane" LastName="Deer"/>
</Window>
```

![Student without DataTemplate](images/student-no-datatemplate.png)

Not very helpful. That's because Avalonia doesn't know _how_ to display an object of type `Student` -
because it's not a control it falls back to just calling `.ToString()` on the object. We can tell
Avalonia how to display non-control objects by defining a data template.

The easiest way to do this on `Window` (and any control that inherits from `ContentControl`) is to
set the [`ContentTemplate`](/api/Avalonia.Controls/ContentControl/7AA9343E) property:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:Example">
  <Window.ContentTemplate>
    <DataTemplate>
      <StackPanel>
        <Grid ColumnDefinitions="Auto,Auto" RowDefinitions="Auto,Auto">
          <TextBlock Grid.Row="0" Grid.Column="0">First Name:</TextBlock>
          <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding FirstName}"/>
          <TextBlock Grid.Row="1" Grid.Column="0">Last Name:</TextBlock>
          <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding LastName}"/>
        </Grid>
      </StackPanel>
    </DataTemplate>
  </Window.ContentTemplate>

  <local:Student FirstName="Jane" LastName="Deer"/>
</Window>
```

![Student first and last name](images/student-first-last-name.png)

The data template for the window content doesn't only come from the `ContentTemplate` property.
Every control also has a `DataTemplates` collection into which any number of data templates can
be placed. If a control doesn't have a template set locally (e.g. in `ContentTemplate`) then it
will look in its `DataTemplates` collection for a matching template. If a match isn't found there
it will then go on to search its parent's `DataTemplates`, then its grandparent's, and so on until
it reaches the `Window`. If it _still_ hasn't found a match it will then look in `App.xaml`/`App.axaml` for
a matching `DataTemplate` and finally when all those options have been exhausted it will simply
call `.ToString()` on the object.

`DataTemplate`s are matched by type: the type that the template matches is specified by setting
the `DataType` property on the template.

> Remember: Each `DataTemplate` in the `DataTemplates` collection should have its `DataType` set
  to the type of the object that it matches, otherwise the data template won't match anything!

Using the `DataTemplates` collection the previous example could be written as:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:Example">
  <Window.DataTemplates>
    <DataTemplate DataType="{x:Type local:Student}">
      <Grid ColumnDefinitions="Auto,Auto" RowDefinitions="Auto,Auto">
        <TextBlock Grid.Row="0" Grid.Column="0">First Name:</TextBlock>
        <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding FirstName}"/>
        <TextBlock Grid.Row="1" Grid.Column="0">Last Name:</TextBlock>
        <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding LastName}"/>
      </Grid>
    </DataTemplate>
  </Window.DataTemplates>

  <local:Student FirstName="Jane" LastName="Deer"/>
</Window>
```

Using this mechanism, if you want to reuse a `DataTemplate` everywhere in a `Window` you can
specify it in `Window.DataTemplates`; if you want the template to be used throughout the whole
application you can specify it in `App.xaml`/`App.axaml` in the `Application.DataTemplates` collection.

Now lets add another view model into the mix:

```csharp
namespace Example
{
    public class Teacher
    {
        public Teacher(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }
}
```

Now we can add a separate data template for the `Teacher` type and depending on the type of object
in the `MainWindowViewModel.Content` property, the appropriate view will be displayed:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:Example">
  <Window.DataTemplates>

    <DataTemplate DataType="{x:Type local:Student}">
      <Grid ColumnDefinitions="Auto,Auto" RowDefinitions="Auto,Auto">
        <TextBlock Grid.Row="0" Grid.Column="0">First Name:</TextBlock>
        <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding FirstName}"/>
        <TextBlock Grid.Row="1" Grid.Column="0">Last Name:</TextBlock>
        <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding LastName}"/>
      </Grid>
    </DataTemplate>

    <DataTemplate DataType="{x:Type local:Teacher}">
      <Grid ColumnDefinitions="Auto,4,Auto">
        <TextBlock Grid.Row="0" Grid.Column="0">Professor</TextBlock>
        <TextBlock Grid.Row="0" Grid.Column="2" Text="{Binding LastName}"/>
      </Grid>
    </DataTemplate>

  </Window.DataTemplates>

  <ContentControl Content="{Binding Content}"/>
</Window>
```

## Evaluation Order

Data templates in Avalonia can target interfaces and derived classes and so the order of
DataTemplates can be important: DataTemplates within the same collection are evaluated in
declaration order so you need to place them from most-specific to least-specific as you would
in code.
