---
Title: The DataContext
Order: 0
---

The [`Control.DataContext`](/api/Avalonia.Controls/Control/D29AE9A9) property
describes where controls will look by default for values when binding. The data context will usually
be set for top-level controls such as [`Window`](http://avaloniaui.net/api/Avalonia.Controls/Window)
and child controls will inherit this data context.

When using the MVVM pattern, the data context will usually be an instance of a view model.

If you created your application with the [Avalonia MVVM Application](/docs/quickstart/create-new-project) template
then you will see something like this in your `Program.cs` file:

```csharp
private static void AppMain(Application app, string[] args)
{
    var window = new MainWindow
    {
        DataContext = new MainWindowViewModel(),
    };

    app.Run(window);
}
```

This means that when the `MainWindow` is created, a new instance of `MainWindowViewModel` will be
created and assigned to the window's `DataContext` property. From here all bindings will by default
bind to properties on this object:

```xml
<Window>
    <Button Content="{Binding ButtonCaption}"/>
</Window>
```

Will bind the `Button`'s `Content` to `Window.DataContext.ButtonCaption`.

## Binding DataContext

When binding `DataContext`, the `DataContext` of the parent control is used as the source of the binding:

```xml
<Window>
    <!-- Will bind `DataContext` to `Window.DataContext.Content -->
    <StackPanel DataContext="{Binding Content}"/>
</Window>
```

Controls that display content based on a [data template](/docs/templates/datatemplate) will automatically
set the `DataContext` for the controls in the template. For example `ContentControl`:

```xml
<Window>
    <ContentControl DataContext="{Binding Content}">
        <ContentControl.ContentTemplate>
            <DataTemplate>
                <!-- Will bind `Text` to `Window.DataContext.Content.Header -->
                <TextBlock Text="{Binding Header}"/>
            </DataTemplate>
        </ContentControl.ContentTemplate>
    </ContentControl>
</Window>
```
