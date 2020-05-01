---
Title: Avalonia
Order: 50
---

Welcome to Avalonia! Avalonia is a cross platform XAML Framework for .NET Framework, .NET Core and Mono.

Avalonia is shipped as .NET standard 2.0 [NuGet packages](/docs/quickstart/packages) and a [set of templates](/docs/quickstart/create-new-project) for Visual Studio and .NET core. We also have a [visual designer](/docs/quickstart/vs-designer) for Visual Studio.

This page is designed as a whirlwind tour of Avalonia. There are plenty of links throughout to more detailed documentation on each area, or you can navigate using the sections on the left.

:::note
If you're familiar with WPF or UWP then you should feel right at home with Avalonia. Although Avalonia is not API compatible with either of these frameworks (and so controls can't be used without porting), there's a lot of similarity. If you're a WPF user we have [a page which describes the main differences between WPF and Avalonia](/docs/quickstart/from-wpf).
:::


## Programming with Avalonia

Avalonia lets you develop an appliction using the [XAML markup language](/docs/quickstart/intro-to-xaml) and C# (or another .NET language) for code. You generally use XAML markup to implement the appearance of an application while using code to implement its behavior.

### Markup

XAML is an XML-based markup language that implements an application's appearance declaratively. You typically use it to create windows and user controls, and to fill them with controls, shapes, and graphics.

The following example uses XAML to implement the appearance of a window that contains a single button:

:::filename
XAML
:::
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow"
        Title="Window with Button"
        Width="250" Height="100">
  
  <!-- Add button to window -->
  <Button Name="button">Click Me!</Button>

</Window>
```

Specifically, this XAML defines a window and a button by using the Window and Button elements, respectively. Each element is configured with attributes, such as the Window element's Title attribute to specify the window's title-bar text. At run time, Avalonia converts the elements and attributes that are defined in markup to instances of Avalonia classes. For example, the Window element is converted to an instance of the Window class whose Title property is the value of the Title attribute.

The following images show the user interface that is defined by the XAML in the previous example running on Windows:

Since XAML is XML-based, the UI that you compose with it is assembled in a hierarchy of nested elements known as an [element tree](/docs/advanced/trees). The element tree provides a logical and intuitive way to create and manage UIs.

![Application window](/docs/quickstart/images/click-me.png)

### Code-behind

The main behavior of an application is to implement the functionality that responds to user interactions, including handling events (for example, clicking a menu, tool bar, or button) and calling business logic and data access logic in response. In Avalonia, this behavior can be implemented in code that is associated with markup. This type of code is known as [code-behind](/docs/quickstart/codebehind). The following example shows the updated markup from the previous example and the code-behind:

:::filename
XAML
:::
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow"
        Title="Window with Button"
        Width="250" Height="100">
  
  <!-- Add button to window -->
  <Button Name="button" Click="button_Click">Click Me!</Button>

</Window>
```

:::filename
C#
:::
```csharp
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            // Change button text when button is clicked.
            var button = (Button)sender;
            button.Content = "Hello, Avalonia!";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
```

In this example, the code-behind implements a class that derives from the [`Window`](/docs/quickstart/window) class. The `x:Class` attribute is used to associate the markup with the code-behind class. `InitializeComponent` is called from the code-behind class's constructor to merge the UI that is defined in markup with the code-behind class. The combination of `x:Class` and `InitializeComponent` ensure that your implementation is correctly initialized whenever it is created. The code-behind class also implements an event handler for the button's `Click` event. When the button is clicked, the event handler changes the text of the button by setting a property on the `Button` control.

### The Model-View-ViewModel Pattern

As well as writing code in code-behind, Avalonia supports using the [Model-View-ViewModel](/docs/quickstart/mvvm) pattern (or MVVM). MVVM is a common way to structure UI applications that separates view logic from application logic in a way that allows your applications to become unit-testable.

MVVM relies upon Avalonia's [binding](/docs/binding) capabilities to separate your application into a View layer which displays standard Avalonia windows and controls, and a ViewModel layer which defines the functionality of the application independently of Avalonia itself. The following example shows the code from the previous example implemented using the MVVM pattern:

:::filename
XAML
:::
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="AvaloniaApplication1.MainWindow"
        Title="Window with Button"
        Width="250" Height="100">

  <!-- Add button to window -->
  <Button Content="{Binding ButtonText}" Command="{Binding ButtonClicked}"/>

</Window>
```

:::filename
C#
:::
```csharp
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        string buttonText = "Click Me!";

        public string ButtonText
        {
            get => buttonText;
            set 
            {
                buttonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ButtonClicked() => ButtonText = "Hello, Avalonia!";
    }
}
```

In this example, the code-behind assigns the `Window`'s [`DataContext`](/docs/binding/datacontext) property to an instance of `MainWindowViewModel`. The XAML then uses an Avalonia [`{Binding}`](/docs/binding/bindings) to bind the `Button`'s `Content` property to the `ButtonText` property on the `MainWindowViewModel`. It also binds the `Button`'s [`Command`](/docs/binding/binding-to-commands) property to the `ButtonClicked` method on the `MainWindowViewModel`.

When the `Button` is clicked it invokes its `Command`, causing the bound `MainWindowViewModel.ButtonClicked` method to be called. This method then sets the `ButtonText` property which raises the `INotifyPropertyChanged.PropertyChanged` event, causing the `Button` to re-read its bound value and update the UI.

## Controls

Avalonia provides many core controls. Here are some of the most common:

- Buttons: [`Button`](/docs/controls/button), [`RepeatButton`](/docs/controls/repeatbutton)
- Data Display: [`ItemsControl`](/docs/controls/itemscontrol), [`ItemsRepeater`](/docs/controls/itemsrepeater), [`ListBox`](/docs/controls/listbox), [`TreeView`](/docs/controls/treeview) 
- Input: [`CheckBox`](/docs/controls/checkbox), [`ComboBox`](/docs/controls/combobox), [`RadioButton`](/docs/controls/radiobutton), [`Slider`](/docs/controls/slider), [`TextBox`](/docs/controls/textbox)
- Layout: [`Border`](/docs/controls/border), [`Canvas`](/docs/controls/canvas), [`DockPanel`](/docs/controls/dockpanel), [`Expander`](/docs/controls/expander), [`Grid`](/docs/controls/grid), [`GridSplitter`](/docs/controls/gridsplitter), [`Panel`](/docs/controls/panel), [`Separator`](/docs/controls/separator), [`ScrollBar`](/docs/controls/scrollbar), [`ScrollViewer`](/docs/controls/scrollviewer), [`StackPanel`](/docs/controls/stackpanel), [`Viewbox`](/docs/controls/viewbox), [`WrapPanel`](/docs/controls/wrappanel)
- Menus: [`ContentMenu`](/docs/controls/contextmenu), [`Menu`](/docs/controls/menu), [`NativeMenu`](/docs/controls/nativemenu)
- Navigation: [`TabControl`](/docs/controls/tabcontrol), [`TabStrip`](/docs/controls/tabstrip)
- User Information: [`ProgressBar`](/docs/controls/progressbar), [`TextBlock`](/docs/controls/textblock), [`ToolTip`](/docs/controls/tooltip)

## Input and Commands

Controls most often detect and respond to user input. The Avalonia [input system](/docs/input) uses both [direct and routed events](/docs/input/events) to support text input, focus management, and mouse positioning.

Applications often have complex input requirements. Avalonia provides a [command system](/docs/binding/binding-to-commands) that separates user-input actions from the code that responds to those actions.

## Layout

When you create a user interface, you arrange your controls by location and size to form a layout. A key requirement of any layout is to adapt to changes in window size and display settings. Rather than forcing you to write the code to adapt a layout in these circumstances, Avalonia provides a first-class, extensible layout system for you.

The cornerstone of the layout system is relative positioning, which increases the ability to adapt to changing window and display conditions. In addition, the layout system manages the negotiation between controls to determine the layout. The negotiation is a two-step process: first, a control tells its parent what location and size it requires; second, the parent tells the control what space it can have.

The layout system is exposed to child controls through base Avalonia classes. For common layouts such as grids, stacking, and docking, Avalonia includes several layout controls:

- [`Panel`](/docs/controls/panel): Child controls are stacked on top of each other to fill the panel
- [`Canvas`](/docs/controls/canvas): Child controls provide their own layout
- [`DockPanel`](/docs/controls/dockpanel): Child controls are aligned to the edges of the panel
- [`Grid`](/docs/controls/grid): Child controls are positioned by rows and columns
- [`StackPanel`](/docs/controls/stackpanel): Child controls are stacked either vertically or horizontally
- [`WrapPanel`](/docs/controls/wrappanel): Child controls are positioned in left-to-right order and wrapped to the next line when there are more controls on the current line than space allows

You can also create your own layouts by [deriving from the `Panel` class](/docs/layout/creating-a-panel).

## Data Binding

Avalonia includes comprehensive support for [binding](/docs/binding/bindings) between controls and to aribtrary .NET objects. Data binding can be set up [in XAML](/docs/binding/bindings) or [in code](/docs/binding/binding-from-code) and supports:

- Multiple binding modes: one way, two way, one-time and one-way to source
- Binding to a [`DataContext`](/docs/binding/datacontext)
- Binding to [other controls](/docs/binding/binding-to-controls)
- Binding to [`Task`s and `Observables`](/docs/binding/binding-to-tasks-and-observables)
- Binding [converters](/docs/binding/converting-binding-values) and negating binding values

The following example shows a `TextBlock` when an associated `TextBox` is disabled, by using a binding:

:::filename
XAML
:::
```xml
<StackPanel>
    <TextBox Name="input" IsEnabled="False"/>
    <TextBlock IsVisible="{Binding !#input.IsEnabled}">Sorry, no can do!</TextBlock>
</StackPanel>
```

In this example, a binding is set up to the `IsEnabled` property of the `input` control using `#input.IsEnabled` and the value of that binding is negated and fed into the `TextBlock.IsVisible` property.

## Styling

[Styles in Avalonia](/docs/styles/styles) are used to share property settings between controls. The Avalonia styling system can be thought of as a mix of CSS styling and WPF/UWP styling. At its most basic, a style consists of a _selector_ and a collection of _setters_. 

The following style selects any `TextBlock` in the `Window` with a `h1` _style class_ and sets its font size to 24
point and font weight to bold:

:::filename
XAML
:::
```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Window.Styles>
        <Style Selector="TextBlock.h1">
            <Setter Property="FontSize" Value="24"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
    </Window.Styles>

    <TextBlock Classes="h1">I'm a Heading!</TextBlock>
</Window>
```

## Graphics

Avalonia introduces an extensive, scalable, and flexible set of graphics features that have the following benefits:

- Resolution-independent and device-independent graphics. The basic unit of measurement in the Avalonia graphics system is the device-independent pixel, which is 1/96th of an inch, regardless of actual screen resolution, and provides the foundation for resolution-independent and device-independent rendering. Each device-independent pixel automatically scales to match the dots-per-inch (dpi) setting of the system it renders on.
- Improved precision. The Avalonia coordinate system is measured with double-precision floating-point numbers rather than single-precision. Transformations and opacity values are also expressed as double-precision.
- Advanced graphics and animation support. Avalonia simplifies graphics programming by managing animation scenes for you; there is no need to worry about scene processing, rendering loops, and bilinear interpolation. Additionally, Avalonia provides hit-testing support and full alpha-compositing support.
- Skia. By default Avalonia uses the [Skia rendering engine](https://skia.org/), the same rendering engine that powers Google Chrome and Chrome OS, Android, Mozilla Firefox and Firefox OS, and many other products.

### 2D Shapes and Geometries

Avalonia provides a library of common vector-drawn 2D shapes such as `Ellipse`, `Line`, `Path`, `Polygon` and `Rectangle`.

:::filename
XAML
:::
```xml
<Canvas Background="Yellow" Width="300" Height="400">
    <Rectangle Fill="Blue" Width="63" Height="41" Canvas.Left="40" Canvas.Top="31">
    <Rectangle.OpacityMask>
        <LinearGradientBrush StartPoint="0%,0%" EndPoint="100%,100%">
        <LinearGradientBrush.GradientStops>
            <GradientStop Offset="0" Color="Black"/>
            <GradientStop Offset="1" Color="Transparent"/>
        </LinearGradientBrush.GradientStops>
        </LinearGradientBrush>
    </Rectangle.OpacityMask>     
    </Rectangle>
    <Ellipse Fill="Green" Width="58" Height="58" Canvas.Left="88" Canvas.Top="100"/>
    <Path Fill="Orange" Data="M 0,0 c 0,0 50,0 50,-50 c 0,0 50,0 50,50 h -50 v 50 l -50,-50 Z" Canvas.Left="30" Canvas.Top="250"/>
    <Path Fill="OrangeRed" Canvas.Left="180" Canvas.Top="250">
    <Path.Data>
        <PathGeometry>
        <PathFigure StartPoint="0,0" IsClosed="True">
            <QuadraticBezierSegment Point1="50,0" Point2="50,-50" />
            <QuadraticBezierSegment Point1="100,-50" Point2="100,0" />
            <LineSegment Point="50,0" />
            <LineSegment Point="50,50" />
        </PathFigure>
        </PathGeometry>
    </Path.Data>
    </Path>
    <Line StartPoint="120,185" EndPoint="30,115" Stroke="Red" StrokeThickness="2"/>
    <Polygon Points="75,0 120,120 0,45 150,45 30,120" Stroke="DarkBlue" StrokeThickness="1" Fill="Violet" Canvas.Left="150" Canvas.Top="31"/>
    <Polyline Points="0,0 65,0 78,-26 91,39 104,-39 117,13 130,0 195,0" Stroke="Brown" Canvas.Left="30" Canvas.Top="350"/>
</Canvas>
```

![Drawing shapes](/docs/quickstart/images/shapes.png)

### Animation

Avalonia's animation support lets you make controls grow, shake, spin, and fade, to create interesting page transitions, and more. Avalonia uses a CSS-like animation system which supports [property transitions](/docs/animations/transitions) and [keyframe animations](/docs/animations/keyframe).
