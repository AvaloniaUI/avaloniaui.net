---
Title: Advanced Tutorial
Order: 7
---

# Avalonia Music Store Tutorial

 Today we will show you how easy it is to build great looking visual desktop applications using JetBrains Rider and Avalonia. The full source for this tutorial can be found at https://github.com/AvaloniaUI/Avalonia.MusicStore.

This guide has instructions for Rider on OSX, however the steps will be almost the same on other operating systems, and reasonably similar on other IDEs such as Visual Studio.

This tutorial acompanies our livestreamed webinar.

Our livestream assumes some knowledge of XAML, MVVM development, however this guide should fill in the gaps for begginers.

![image-20210310184538120](images/image-20210310184538120.png)



## A little background to Avalonia

Avalonia is a "Template" based UI framework. This means that controls have `Templates` that describe how they look on the screen.

This is a different approach to other UI frameworks where native controls are used, for example a `Button` will take on the standard look of a button on its respective operating system.

Some apps require or desire a native look and feel. Others require a "pixel perfect" style and design. This pixel perfect approach is becoming more popular and common place. Good examples of this kind of application are "Slack" where the companies branding is clear throughout the UI. It also looks the exact same no matter which OS you run it on.

Avalonia is suited to these "Pixel Perfect" style apps, providing the advantage of native code and speed.

If you are already familiar with MVVM you may wish to skip this next section, if your new to Avalonia read on.



### Model View ViewModel

The best architecture to use Avalonia with (not compulsory, just works best) is MVVM (**M**odel **V**iew **V**iew**M**odel).

It sounds complicated, and there are many over complicated guides and tutorials on the internet.

MVVM is simply a way to enforce [Separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns). For your quick demo tutorial app, this may seem overkill. Keeping UI and buisness logic separated in such a way. However the apps that your will soon be building, often start small but quickly grow. Your customers will spring new requirements on you, and you will need to shoe horn them into your software.

Following the MVVM approach will alleviate these difficulties and help keep your UI code scalable.



![img](images/mvvm.png)



#### How does MVVM work?

Back to seperation of concerns, in any GUI application there are at least 3 main concerns:

- UI - Layout, Style, Content

- UI Logic - How the controls interact with each other and the user.

- Buisness Logic - The actual functionality your application provides, dealing with a database, controlling some hardware you built for IOT, ordering products from your store.

  

##### Why is it a good idea to keep these separated?

If your code combines or mixes these 3 aspects together, it will tie the design of your UI directly to your buisness logic. This will make it incredibly difficult to make large changes to the way your application works. This was common place in the not too distant past.



##### How does MVVM acheive this?

###### Model (Buisness Logic)

All your buisness logic can be contained in plain classes, they can be designed and implemented however you want. Most project you already may have this. The general term we call these buisness logic classes is `Models`.

Your models can simply expose `methods` (functions), `properties` and use `events` to notify other parts of the MVVM architecture when something has changed in the system.

For example if your domain, is a music store. Perhaps your buisness logic provides a list of the top 10 albums. It may happen that the list changes and this change can be propogated by use of an `event`.

So now we have the `Model` part of MVVM, all self contained, the `models` know nothing about any of the other parts.



######  ViewModel (UI Logic)

You user interface is essentially the way your `users` interact with your buisness logic or `models`. There needs to be a way for your UI to interact with the buisness logic. We do this using a `ViewModel`. A `ViewModel` knows about the `Model` that it represents. It does not know anything about the layout or design of the UI or `View` part.

A `ViewModel` is essentially special type of `Model` that represents all the `data` that will be displayed in the UI. It also represents all the `actions` that can be done with the UI. For example what happens when a button is clicked.

This keeps things like disabling buttons when the user hasnt input the correct information away from your buisness logic.

A `ViewModel` will `subscribe` to or `observe` events on a model so that it knows when something in your system has changed, with the intention that it can then update the UI so the `user` will know about it.

A `ViewModel` will also call or trigger functionality in response to user input like a `Button` being `clicked`.



###### View (UI)

The view provides the layout and content of the UI. It knows about the `ViewModel` and the information it provides that can be displayed in the UI.

The view describes how to "present" data and provides controls the user can interact with.

A `View` can retrieve `data` to display with the use of `Bindings` . `Bindings` can also be used for `interactions` to be communicated back to the `ViewModel` .



###### Summary

A model provides the buisness logic and talks to the ViewModel.

A ViewModel provides the UI logic and invokes the buisness logic in response to user input.

A View provides the look, layout and content of the UI.



Lets get started building something!
