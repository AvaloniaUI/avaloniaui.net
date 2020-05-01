---
Title: Avalonia is Now in Alpha
Published: 2015-08-31
Category: Release
Author: Steven Kirk
---

I'm pleased to announce that [Avalonia](https://github.com/grokys/Avalonia/) is now
in alpha!

Avalonia is a multi-platform windowing toolkit - somewhat like WPF - that is
intended to be multi-platform (more about that below). It supports XAML,
lookless controls and a flexible styling system, and runs on Windows using
Direct2D and other operating systems using Gtk & Cairo.

So what does "alpha mean? Well, it means that it's now at a stage where you
can have a play and hopefully create simple applications. There's now a [Visual
Studio Extension](https://visualstudiogallery.msdn.microsoft.com/87db356c-cec9-4a07-b7db-a4ed8a921ac9)
containing project and item templates that will help you get started, and
there's an initial complement of controls. There's still a lot missing, and you
*will* find bugs, and the API *will* change, but this represents the first time
where we've made it somewhat easy to have a play and experiment with the
framework.

## How do I try it out

The easiest way to try out Avalonia is to install the [Visual Studio Extension](https://visualstudiogallery.msdn.microsoft.com/87db356c-cec9-4a07-b7db-a4ed8a921ac9).

This will add a Avalonia project template and a Window template to the standard
Visual Studo "Add" dialog (yes, icons still to come :) ):

![Add Dialogs](/blog/images/2015-08-31-avalonia-alpha/add-dialogs.png)

Creating a Avalonia Project will give you a simple project with a single XAML
window. There's currently no designer, and not even any type-checking or
intellisense for Avalonia's xaml, but it works when you press F5, which is the
important part!

![Add Dialogs](/blog/images/2015-08-31-avalonia-alpha/hello-world-xaml.png)

There's also a Avalonia Window template in there, but no User Control template
yet (in reality you just need to change `Window` to `UserControl`).

## Multi-platform you say?

Well, yes, that is the intention. However unfortunately as of the time of this
first alpha, Avalonia is only shipping with a Windows backend. There *is* a
Gtk/Cairo backend that's working pretty well (at least on Windows) but it's not
included in this release due to packaging issues. In addition, the framework did
work on Linux at one point but with the recent Mono 4.0 something has gone
wrong, and we need time to work out what that is. Getting Avalonia working again
on non-windows support is the next thing we'll be concentrating on. You can
track the progress on Linux in the [issue on GitHub](https://github.com/grokys/Avalonia/issues/78).

Even though it's not quite there yet, the reason it's a close as it is is due to
the efforts of [ncarillo](https://github.com/ncarrillo) - thanks Nelson!

## OmniXAML

Avalonia's XAML support has only just landed and its only thanks to the great
[OmniXaml](https://github.com/SuperJMN/OmniXAML) project that it's at all
possible. OmniXAML makes it possible to consume XAML from PCLs and best of all,
the XAML support is *completely* decoupled from the main Avalonia library,
due to OmniXaml's high degree of configurability meaning that Avalonia could well
support other types of markup in future. It's still early days, but check out
OmniXAML now! Thanks [@José ](https://github.com/SuperJMN)!
