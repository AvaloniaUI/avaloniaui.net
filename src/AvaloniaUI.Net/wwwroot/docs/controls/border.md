---
Title: Border
---
The `Border` control decorates a child with a border and background. It can also be used to
display rounded corners by setting the [`CornerRadius`](/api/Avalonia.Controls/Border/60DC8BED)
property.

An example of a border with a red background, 2 pixel black border, 3 pixel corner radius and a
4 pixel padding around its content:

```xml
<Border Background="Red"
        BorderBrush="Black"
        BorderThickness="2"
        CornerRadius="3"
        Padding="4">
    <StackPanel>
        <Button>Button 1</Button>
        <Button>Button 2</Button>
    </StackPanel>
</Border>
```

## Common Properties

|Property|Description|
|--------|-----------|
|`Background`|A `Brush` describing the color of the control's background|
|`BorderBrush`|A `Brush` describing the color of the control's border stroke|
|`BorderThickness`|The thickness of the control's border stroke|
|`Child`|The child control to decorate|
|`CornerRadius`|The radius of the border's rounded corners|

## Pseudoclasses

None

## Source code
[Border.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Border.cs)
