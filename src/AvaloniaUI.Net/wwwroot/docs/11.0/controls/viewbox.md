---
Title: Viewbox
---

The `Viewbox` is a decorator control which scales its child. It can be used to scale its child to fit the available space.

Though it can be used for any kind of control, it is often used to scale 2D images to a given space.

The `Viewbox` gives its child infinite space in the measure phase. It will constrain either or both sides when aranging it.
This depends on the Value of the `Stretch` property.

Here is an example of a button scaled up to fit the given space. Without the `Viewbox` the button would have
been rendered at around 22 dip height. With the `Viewbox` it will be displayed with a height of 300 dip:

```xml
<!-- button will occupy 22 dip under usual font sizes and dpi setting -->
<Button Content="Text"/>  

<!-- this button will be much larger -->
<Viewbox Stretch="Uniform" Height="300">
	<Button Content="Text"/>
</Viewbox>
```

# Common Properties

|Property|Description|
|--------|-----------|
|`Stretch`|One of the following values, determining the mode of scaling.|

## Values for `Stretch`
|Value        |Description|
|-------------|-----------|
|None         |The content preserves its original size.|
|Fill         |The content is resized to fill the destination dimensions. The aspect ratio is not preserved.|
|Uniform      |The content is resized to fit in the destination dimensions while preserving its native aspect ratio.|
|UniformToFill|The content is resized to completely fill the destination rectangle while preserving its native aspect ratio. A portion of the content may not be visible if the aspect ratio of the content does not match the aspect ratio of the allocated space.|

## Pseudoclasses
None

## Reference
[Viewbox](http://reference.avaloniaui.net/api/Avalonia.Controls/Viewbox/)

## Source code
[Viewbox.cs](https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Controls/Viewbox.cs)
