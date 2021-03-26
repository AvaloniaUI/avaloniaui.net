---
Title: Setup Development Environment
Order: 1
---

## Setting up your development environment

1. Download and install .NET 5 SDK [Download .NET (Linux, macOS, and Windows) (microsoft.com)](https://dotnet.microsoft.com/download)

   This is the runtime, development kit (compiler, etc) that is used to build Avalonia applications.

   

2. Install Avalonia Templates

   Run the command `dotnet new -i Avalonia.Templates` from the command prompt on your machine.

   The output will look similar to this.

   ```bash
   $ dotnet new -i Avalonia.Templates
     Determining projects to restore...
     Restored /Users/danwalmsley/.templateengine/dotnetcli/v5.0.200/scratch/restore.csproj (in 706 ms).
   
   Templates                                     Short Name            Language    Tags
   .....
   
   Avalonia Resource Dictionary                  avalonia.resource                 ui/xaml/avalonia/avaloniaui
   Avalonia Styles                               avalonia.styles                   ui/xaml/avalonia/avaloniaui
   
   Examples:
       dotnet new mvc --auth Individual
       dotnet new mstest
       dotnet new --help
       dotnet new avalonia.mvvm --help
   $
   ```

   

3. Download and install [Rider: The Cross-Platform .NET IDE from JetBrains](https://www.jetbrains.com/rider/)

   Rider will give you the very best development experience available for Avalonia. It is available for Windows, Linux and Mac

   

4. Install the Avalonia Plugin

   Once Rider loads you will see the Welcome Screen. Click the `Configure` dropdown and select `Plugins`. 

<img src="/Users/danwalmsley/repos/avaloniaui.net/src/AvaloniaUI.Net/wwwroot/docs/advanced-tutorial/images/rider-welcome.png" alt="rider-welcome" style="zoom:50%;" />

A new Preferenes Screen will open up. Click the `Settings` icon as shown and select `Manage Plugin Repositories...`

![configure-plugin-repos](/Users/danwalmsley/repos/avaloniaui.net/src/AvaloniaUI.Net/wwwroot/docs/advanced-tutorial/images/configure-plugin-repos.png)

Click the `+` icon and enter the url `https://plugins.jetbrains.com/plugins/dev/14839`then click `OK`

<img src="/Users/danwalmsley/repos/avaloniaui.net/src/AvaloniaUI.Net/wwwroot/docs/advanced-tutorial/images/enter-plugin-repo.png" alt="enter-plugin-repo" style="zoom:50%;" />

Now click on the `Marketplace` tab and search for `Avalonia`. Select `AvaloniaRider` and click `Install` then once thats done, click the `Restart IDE` button that will appear.

<img src="/Users/danwalmsley/repos/avaloniaui.net/src/AvaloniaUI.Net/wwwroot/docs/advanced-tutorial/images/plugin-install.png" alt="plugin-install" style="zoom:50%;" />



Now Rider should be ready to develop Avalonia applications.