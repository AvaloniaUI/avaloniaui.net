---
Title: ListBox
---
The `ListBox` is an `ItemsControl` which displays items in a multi-line list box and allows individual selection.

The items to display in the `ListBox` are specified using the `Items` property. This property will
often be bound to a collection on the control's `DataContext`:

```xml
<ListBox Items="{Binding MyItems}"/>
```

## Customizing the item display

You can customize how an item is displayed by specifying an `ItemTemplate`. For example to display
each item inside a red border with rounded corners:

```xml
<ListBox Items="{Binding MyItems}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Border Background="Red" CornerRadius="4" Padding="4">
                <TextBlock Text="{Binding}"/>
            </Border>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

## Preventing Horizontal Scrolling

By default if an item is too wide to display in the `ListBox`, a horizontal scrollbar will be
displayed. If instead you want items to be constrained to the width of the `ListBox` (for example
if you want wrapping text in the items) you can disable the horizontal scrollbar by setting
`ScrollViewer.HorizontalScrollBarVisibility="Disabled"`.

```xml
<ListBox Items="{Binding MyItems}" Width="250" ScrollViewer.HorizontalScrollBarVisibility="Disabled">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Border Background="Red" CornerRadius="4" Padding="4">
                <TextBlock Text="{Binding}" TextWrapping="Wrap"/>
            </Border>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>

```

## Source code
[ListBox.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/ListBox.cs)
