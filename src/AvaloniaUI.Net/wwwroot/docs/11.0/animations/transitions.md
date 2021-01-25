---
Title: Transitions
Order: 20
---

Transitions in Avalonia are also heavily inspired by CSS Animations. They listen to any changes in target property's value 
and subsequently animates the change according to its parameters. They can be defined on any `Control` via `Transitions` property:

```xml
<Window xmlns="https://github.com/avaloniaui">
    <Window.Styles>
        <Style Selector="Rectangle.red">
            <Setter Property="Height" Value="100"/>
            <Setter Property="Width" Value="100"/>
            <Setter Property="Background" Value="Red"/>
            <Setter Property="Opacity" Value="0.5"/>
        </Style>
        <Style Selector="Rectangle.red:pointerover">
            <Setter Property="Opacity" Value="1"/>
        </Style>
    </Window.Styles>

    <Rectangle Classes="red">
        <Rectangle.Transitions>
            <DoubleTransition Property="Opacity" Duration="0:0:0.2"/>
        </Rectangle.Transitions>
    </Rectangle>

</Window>
```

The above example will listen to changes in the `Rectangle`'s `Opacity` property, and when the
value changes, apply a smooth transition from the old value to the new value over 2 seconds.

Transitions can also be defined in any style by using a `Setter` with `Transitions` as the
target property and encapsulating them in a `Transitions` object, like so:

```xml
<Window xmlns="https://github.com/avaloniaui">
    <Window.Styles>
        <Style Selector="Rectangle.red">
            <Setter Property="Height" Value="100"/>
            <Setter Property="Width" Value="100"/>
            <Setter Property="Background" Value="Red"/>
            <Setter Property="Opacity" Value="0.5"/>
            <Setter Property="Transitions">
                <Transitions>
                    <DoubleTransition Property="Opacity" Duration="0:0:0.2"/>
                </Transitions>
            </Setter>
        </Style>
        <Style Selector="Rectangle.red:pointerover">
            <Setter Property="Opacity" Value="1"/>
        </Style>
    </Window.Styles>

    <Rectangle Classes="red"/>

</Window>
```

Every transition has a `Property`, `Delay`, `Duration` and an optional `Easing` property. 

`Property` refers to a transition's target for listening and animating values upon.

`Delay` refers to the amount of time before the transition is applied to the target.

`Duration` refers to the amount of time that the transition plays.

The easing functions are the same as those described in [Keyframe Animations](keyframe#easings).

The following transition types are available. The correct type must be used depending on the type
of the property being animated.

* `DoubleTransitions`: For `double` target properties
* `FloatTransitions`: For `float ` target properties
* `IntegerTransitions`: For `int` target properties
