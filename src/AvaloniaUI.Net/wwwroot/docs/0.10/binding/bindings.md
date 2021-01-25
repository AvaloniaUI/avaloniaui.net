---
Title: Bindings
Order: 10
---

You bind in XAML using the `{Binding}` markup extension. By using bindings (assuming you've
implemented [change notifications](change-notifications)) any changes to the data context will
automatically be updated in the control.

By default a binding binds to a property on the [`DataContext`](datacontext), e.g.:

```xml
<!-- Binds to the TextBlock's DataContext.Name property -->
<TextBlock Text="{Binding Name}"/>

<!-- Which is the same as ('Path' is optional) -->
<TextBlock Text="{Binding Path=Name}"/>
```

An empty binding binds to DataContext itself

```xml
<!-- Binds to the TextBlock's DataContext property -->
<TextBlock Text="{Binding}"/>

<!-- Which is the same as -->
<TextBlock Text="{Binding .}"/>
```

We call the property on the control the binding _target_ and the property on the `DataContext` the
binding _source_.

## Binding Path

The binding path above can be a single property, or it can be a chain of properties. For example
if the object assigned to the `DataContext` has a `Student` property, and the value of this property
has a `Name`, you can bind to the student name using:

```xml
<TextBlock Text="{Binding Student.Name}"/>
```

You can also include array/list indexers in binding paths:

```xml
<TextBlock Text="{Binding Students[0].Name}"/>
```

## Binding Modes

You can change the behavior of a `{Binding}` by specifing a binding `Mode`:

```
<TextBlock Text="{Binding Name, Mode=OneTime}">
```

The available binding modes are:

| Mode | Description |
| ---- | ----------- |
| `OneWay` | Changes to the source are automatically propagated to the target |
| `TwoWay` | Changes to the source are automatically propagated to the target and vice-versa |
| `OneTime` | The value from the source is propagated at initialization to the target and subsequent changes are ignored |
| `OneWayToSource` | Changes to the target are propagated to the source |
| `Default` | The binding mode is based on the property |

The `Default` mode is assumed if one is not specified. This mode is generally `OneWay` for control
properties that do not change due to user input (e.g. `TextBlock.Text`) and `TwoWay` for control
properties that _do_ change due to user input (e.g. `TextBox.Text`).
