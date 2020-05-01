---
Title: Binding in a Control Template
Order: 50
---

When you're creating a control template and you want to bind to the templated parent you can use:

```xml
<TextBlock Name="tb" Text="{TemplateBinding Caption}"/>

<!-- Which is the same as -->
<TextBlock Name="tb" Text="{Binding Caption, RelativeSource={RelativeSource TemplatedParent}}"/>
```

Although the two syntaxes shown here are equivalent in this case, there is a difference.
`TemplateBinding` accepts only a single property rather than a property path, so if you want
to bind using a property path you must use the second syntax:

```xml
<!-- This WON'T work as TemplateBinding only accepts single properties -->
<TextBlock Name="tb" Text="{TemplateBinding Caption.Length}"/>

<!-- Instead this syntax must be used in this case -->
<TextBlock Name="tb" Text="{Binding Caption.Length, RelativeSource={RelativeSource TemplatedParent}}"/>
```
