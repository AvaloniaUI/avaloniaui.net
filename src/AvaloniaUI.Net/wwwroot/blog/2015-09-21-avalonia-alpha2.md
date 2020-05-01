---
Title: Avalonia Alpha 2
Published: 2015-09-21
Category: Release
Author: Steven Kirk
---

We're pleased to announce that alpha 2 of
[Avalonia](https://github.com/grokys/Avalonia/) is now available.

Avalonia is a cross platform .NET UI framework inspired by WPF, with XAML, data
binding, lookless controls and much more. Take a look at the video to see our
current progress:

[![Avalonia Alpha 2](http://i.imgur.com/gYqwl0o.png)](https://www.youtube.com/watch?v=c_AB_XSILp0 "Avalonia Alpha 2")

Here are some of the highlights of this release - we've come a long way in the
3 weeks since alpha 1!

## Visual Studio Designer

[Nikita Tsukanov](https://github.com/kekekeks) and [Darnell Williams](https://github.com/ImaBrokeDude) have done the impossible and
got a basic designer for Visual Studio working. It's still early days and not
everything is supported yet, but it's a massive step towards a user-friendly
designer experience with Avalonia. Take a look at the video above to see it in
action or download the [VS Plugin](https://visualstudiogallery.msdn.microsoft.com/a4542e8a-b56c-4295-8df1-7e220178b873)

## Styles in XAML

We've added support for expressing styles in XAML.

```xml
<StackPanel>
  <StackPanel.Styles>
    <Style Selector="Button:pointerover">
      <Setter Property="Button.Foreground" Value="Red"/>
    </Style>
  </StackPanel.Styles>
  <Button>I will have red text when hovered.</Button>
</StackPanel>
```

To see more examples of what you can do with Avalonia styles, [check out the documentation](https://github.com/Avalonia/Avalonia/blob/master/docs/styles.md)

Of course, our XAML support is only available thanks to [OmniXAML](https://github.com/superjmn/omnixaml)!

## \*Nix support

Avalonia now runs on \*nix platforms using mono - this includes Linux and Mac
OSX. For information on building Avalonia on \*nix take a look at the [build
instructions](https://github.com/Avalonia/Avalonia/blob/master/docs/build.md).

## HTML View

As well as doing the impossible once with the designer, [Nikita Tsukanov](https://github.com/kekekeks) has also ported the [HTML Renderer](https://htmlrenderer.codeplex.com/) component to Avalonia, giving us a
100% managed HTML 4.01 and CSS 2 renderer directly in Avalonia.

## New Controls Showcase

![Controls Showcase](/blog/2015-09-21-avalonia-alpha2/controls-showcase.png)

[Nelson Carrillo](https://github.com/ncarrillo) (as well as hammering our Cairo
backend into shape) has contributed a new controls showcase to Avalonia. This
is a lot better looking than our previous cobbled-togther test application and
should give a good idea of what Avalonia is capable of at the moment.

## New Features

The following new features have been implemented:

- ImageBrush
- VisualBrush
- Clipboard
- Canvas (thanks to [Amer Koleci](https://github.com/amerkoleci))
- Cursor support

## Nightly NuGet packages

We are now hosting nightly NuGet packages on [MyGet](https://www.myget.org/F/avalonia-nightly/api/v2/Packages).

## Join us

If you're wanting to join in, take a look at the [`up-for-grabs` issues on
GitHub](https://github.com/Avalonia/Avalonia/labels/up-for-grabs) - these issues
are an ideal place to start for a newcomer - then come join us in our [Gitter Room](https://gitter.im/Avalonia/Avalonia)
and let us know what you're thinking of doing.
