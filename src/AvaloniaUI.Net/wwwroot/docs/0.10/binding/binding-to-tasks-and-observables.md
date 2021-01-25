---
Title: Binding to Tasks and Observables
Order: 40
---

You can subscribe to the result of a task or an observable by using the `^` stream binding operator.

For example if `DataContext.Name` is an `IObservable<string>` then the following example will bind
to the length of each string produced by the observable as each value is produced

```xml
<TextBlock Text="{Binding Name^.Length}"/>
```
