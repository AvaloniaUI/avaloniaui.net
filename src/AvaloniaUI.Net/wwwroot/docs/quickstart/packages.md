---
Title: Packages
Order: 15
---

## Stable Releases

Avalonia stable releases are distributed via NuGet.org. The main packages are:

- [Avalonia](https://www.nuget.org/packages/Avalonia/) contains the core framework
- [Avalonia.Desktop](https://www.nuget.org/packages/Avalonia.Desktop/) contains libraries for running the framework on Windows, Linux and Mac OSX
- [Avalonia.ReactiveUI](https://www.nuget.org/packages/Avalonia.ReactiveUI/) contains support for the [ReactiveUI](https://reactiveui.net/) MVVM framework

The other Avalonia packages you see listed on NuGet.org are sub-packages installed by the above.

## Unstable Releases

We also have a [nightly build feed](https://www.myget.org/F/avalonia-ci/api/v2) which tracks the current state of master. Although these packages are less stable than the release on NuGet.org, you'll get all the latest features and bugfixes right away and many of our users actually prefer this feed!

To use the nightly build feed, add a `nuget.config` file to your project, in the directory which contains your solution:

:::filename
nuget.config
:::
```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>
  <packageSources>
    <add key="AvaloniaCI" value="https://www.myget.org/F/avalonia-ci/api/v2" />
  </packageSources>
</configuration>
```

And then in the NuGet package manager, select the `AvaloniaCI` package source.

## PR Feed

Pull request build results are published to a [separate feed](https://www.myget.org/F/valonia-prs/api/v3/index.json).

To get the version for a particular pull request you need to check the build number from the PR build on Azure Pipelines. Then you can use this build to determine the PR package version.

:::note
This feed contains packages with **UNTRUSTED** source code (anyone can create a PR), so be sure to read the diff of the corresponding pull request. NuGet packages can contain malicious code even in build-time scripts.
:::
