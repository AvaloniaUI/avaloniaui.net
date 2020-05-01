---
Title: Changing the Target Framework
Order: 10
---
By default, our project templates target the following frameworks:

- **Visual Studio Extension**: Dual targets .NET 4.6.1 and .NET Core 2.0
- **.NET Core Templates**: .NET Core 2.0

You can change the frameworks that your project targets by opening the `.csproj` file and editing
the `<TargetFramework>` or `<TargetFrameworks>` element.

When a single target framework is required, use the `TargetFramework` element. If you want to target
multiple target frameworks from the same project you should use `TargetFrameworks` and separate the
frameworks with a `;`.

Some examples:

```xml
<TargetFramework>netcoreapp2.1</TargetFramework>
<TargetFramework>net461</TargetFramework>
<TargetFrameworks>net461;netcoreapp2.1</TargetFrameworks>
```

For more information on target frameworks, see [the .NET documentation](https://docs.microsoft.com/en-us/dotnet/standard/frameworks).
