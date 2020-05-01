---
Title: Avalonia Alpha 3
Published: 2015-12-03
Category: Release
Author: Steven Kirk
---

We're pleased to announce that alpha 3 of
[Avalonia](https://github.com/Avalonia/Avalonia/) is now available.

Avalonia is a cross platform .NET UI framework inspired by WPF, with XAML, data
binding, lookless controls and much more. Take a look at the video to see our
current progress:

[![Avalonia Alpha 3](/blog/images/2015-12-03-avalonia-alpha3/video-thumb.png)](https://www.youtube.com/watch?v=NJ9-hnmUbBM "Avalonia Alpha 3")

Get the alpha through [NuGet](https://www.nuget.org/packages/avalonia) and
[download our Visual Studio extension](https://visualstudiogallery.msdn.microsoft.com/a4542e8a-b56c-4295-8df1-7e220178b873) to get started.

## Skia Backend and Initial Support for Android and iOS

[Kekekeks](https://github.com/kekekeks) has implemented a new
[skia](https://skia.org/)-based backend that going forward should provide a
multi-platform renderer that also works on mobile devices. The Direct2D backend
is also being improved, but our Cairo backend is currently lacking a maintainer.
If this sounds like it might interest you, come let us know in our [Gitter room](https://gitter.im/Avalonia/Avalonia) and help keep it
alive!

Kekekeks has also implemented initial support for iOS and [donandren](https://github.com/donandren) sent a PR with Android support.
Avalonia on mobile is still missing important features, but it's a promising
start. Both mobile platforms are currently using the skia-based backend.

## XAML Binding Improvements

XAML binding was rather buggy and lacking in features in the last alpha, but
should now be a lot more usable. We've also added some features above and beyond
traditional XAML binding:

## Binding to Controls

You can now bind to controls:

```xml
    <TextBox Text={Binding ElementName=other, Path=Text}
```

But in addition we also have the shorthand form of:

```xml
    <TextBox Text={Binding #other.Text}
```

## Binding negation

Ever lamented the hoops you have to jump though in XAML to negate a binding?
Well we feel your pain and now you can negate bindings just by adding a `!`
to the binding path:

```xml
    <TextBlock IsVisible="{Binding Loading}">Loading...</TextBlock>
    <!-- We can negate the Loading property here using a '!' -->
    <ContentPresenter Content="{Binding}" IsVisible="{Binding !Loading}"/>
```

## MultiBinding and Async Bindings

We also have initial support for `MultiBinding` (only one-way at the moment) and
you can also bind to `Task` and `Observable` and get the results you'd expect!

## ListBox Multi-Select

`ListBox` now supports multiple selection, and unlike WPF the multiple selection
can be bound two-way to a view model, enabling you to keep a simple list of
selected items without [`IsSelected` container `Style` tricks](http://stackoverflow.com/questions/2511708/databinding-a-listbox-with-selectionmode-multiple).

## XAML Control Themes

Control themes can now be defined in XAML, and the default theme has now been
ported in its entirety to XAML. This is important because it means our XAML
support is coming up to speed!

## Designer Improvements

Many improvements have been made to the [Visual Studio Designer extension](https://visualstudiogallery.msdn.microsoft.com/a4542e8a-b56c-4295-8df1-7e220178b873)
including:

- Zoom
- Background color configuration
- Attached property intellisense
- Markup extension intellisense
- `clr-namespace` support
- `Design.DataContext`, `Design.Width` and `Design.Height` for setting
  design-time properties.

## Direct and Read-Only Avalonia Properties

`AvaloniaProperty`, like `DependencyProperty` is quite heavyweight, so we've
added support for turning standard C# properties into Avalonia properties, which
also gives us convenient read-only properties. Read more about it in [the
documentation](https://github.com/Avalonia/Avalonia/wiki/Registering-AvaloniaProperties#readonly-avaloniaproperties).

## Conclusion

Things are moving fast, but we're still only at alpha 3, so there are still many
things broken, missing features, performance problems, missing documentation and
quite a few memory leaks. We'll be continuing to work on these as we move
forward, but please file any bugs you find on [our issue tracker](https://github.com/Avalonia/Avalonia/issues) and come join us in our
[Gitter room](https://gitter.im/Avalonia/Avalonia).
