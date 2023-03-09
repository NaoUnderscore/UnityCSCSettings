C# Compiler Settings For Unity
===

**:warning: NOTE: Do not use [the obsolete tags and branches](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/issues/10) to reference the package. They will be removed in near future. :warning:**

Change the C# compiler (csc) used on your Unity project, as you like!

[![](https://img.shields.io/npm/v/com.coffee.csharp-compiler-settings?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.coffee.csharp-compiler-settings/)
[![](https://img.shields.io/github/v/release/mob-sakai/CSharpCompilerSettingsForUnity?include_prereleases)](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/releases)
[![](https://img.shields.io/github/release-date/mob-sakai/CSharpCompilerSettingsForUnity.svg)](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/releases)  [![](https://img.shields.io/github/license/mob-sakai/CSharpCompilerSettingsForUnity.svg)](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/blob/master/LICENSE.txt)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-orange.svg)](http://makeapullrequest.com)  
![](https://img.shields.io/badge/Unity%202018.3%20or%20later-supported-blue.svg)
![](https://img.shields.io/badge/Unity%202019.x-supported-blue.svg)
![](https://img.shields.io/badge/Unity%202020.x-supported-blue.svg)  
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/mob-sakai/CSharpCompilerSettingsForUnity/unity-test)
[![Test](https://mob-sakai.testspace.com/spaces/130862/badge?token=43a50d2fc998aa362d36934597de0c84527e5690)](https://mob-sakai.testspace.com/spaces/130862)
[![CodeCoverage](https://mob-sakai.testspace.com/spaces/130862/metrics/103206/badge)](https://mob-sakai.testspace.com/spaces/130862/current/Code%20Coverage)

<< [Description](#description) | [Installation](#installation) | [Usage](#usage) | [Contributing](#contributing) >>

<br><br><br><br>

## Description

A good news! [Unity 2020.2.0a12 or later now supports C# 8.0!](https://forum.unity.com/threads/unity-c-8-support.663757/page-3#post-5811175)

Many developers (including you!) have been eagerly awaiting support for C# 8.0.  
C# 8.0 includes [some features and some useful syntax-sugars](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8):

* Readonly members
* Default interface methods
* Pattern matching enhancements:
  * Switch expressions
  * Property patterns
  * Tuple patterns
  * Positional patterns
* Using declarations
* Static local functions
* Disposable ref structs
* Nullable reference types
* Asynchronous streams
* Asynchronous disposable
* Indices and ranges
* Null-coalescing assignment
* Unmanaged constructed types
* Stackalloc in nested expressions
* Enhancement of interpolated verbatim strings

However, unfortunately, [there are no plans to backport to Unity 2020.1 or earlier...](https://forum.unity.com/threads/unity-c-8-support.663757/page-5#post-6269856)

<br>

This package changes the C# compiler (csc) used on your Unity project, to support C# 8.0, C# 9.0 and more.  
Let's enjoy new C# features with your Unity project!

![](https://user-images.githubusercontent.com/12690315/147154292-934d64f3-7cb4-43d5-8f43-3b9eab4431c6.png)
![](https://user-images.githubusercontent.com/12690315/147154296-bf04b026-8270-4b8f-91ce-0b546fc764c3.png)

### Features

* Easy to use.
  * This package is out of the box!
* Change the C# compiler (csc) used on your Unity project.
  * Change the nuget package name.
    * **[Microsoft.Net.Compilers][]: Official compiler (default in legacy Unity, run on Unity built-in mono)**
    * [Microsoft.Net.Compilers.Toolset][]: Official compiler (default in modern Uniity, run on dotnet)
      * Resolve the [issue #2](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/issues/2)
    * [OpenSesame.Net.Compilers][]: Allows access to internals/privates in other assemblies (run on Unity built-in mono)
    * [OpenSesame.Net.Compilers.Toolset][]: Allows access to internals/privates in other assemblies (run on dotnet)
      * Resolve the [issue #2](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/issues/2)
    * Or, your custom nuget package
  * Change the nuget package version.
    * 3.4.0: C# 8.0 Supported.
    * **3.5.0: C# 8.0 Supported. (default in Unity 2020.2.0)**
    * 3.6.0: C# 8.0 Supported.
    * 3.7.0: C# 8.0 Supported.
    * 3.8.0 (preview): C# 9.0 Supported.
    * **3.9.0: C# 9.0 Supported. (default in Unity 2021.2.0)**
    * 3.10.0: C# 9.0 Supported.
    * 3.11.0: C# 9.0 Supported.
    * 4.0.1: C# 10.0 Supported.
    * For other versions, see the nuget package page above.
  * Change the C# language version.
    * 7.0
    * 7.1
    * 7.2
    * 7.3
    * **8.0 (default in Unity 2020.2.0)**
    * **9.0 (default in Unity 2021.2.0)**
    * 10.0 (latest)
* Add the scripting define symbols based on language version on compiling.
  * e.g. `CSHARP_7_3_OR_LATER`, `CSHARP_8_OR_LATER`, `CSHARP_9_OR_LATER` ...
* Change the C# compiler settings for each `*.asmdef` file.
  * Portability: The assembly works even in the projects that do not have this package installed.
    * The best option when distributing as a package.
  * Publish as Dll: Published dll works without this package.
  * Modify the scripting define symbols for each `*.asmdef` file.
    * Add/Remove specific symbols separated with semicolons (`';'`) or commas (`','`).
    * The symbols starting with `'!'` will be removed.
    * e.g. `SYMBOL_TO_ADD;!SYMBOL_TO_REMOVE;...`
* Modify `langversion` property in *.csproj file.
* If `dotnet` is required, it will be installed automatically.
* `CompilerType.BuiltIn` compiler option to disable this plugin.
* `Enable Logging` option to display compilation log.
* `Nullable` option to enable [Nullable reference types (C# 8.0)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types).
  * `enable`: The nullable annotation context is enabled. The nullable warning context is enabled.
    * Variables of a reference type, string for example, are non-nullable. All nullability warnings are enabled.
  * `warnings`: The nullable annotation context is disabled. The nullable warning context is enabled.
    * Variables of a reference type are oblivious. All nullability warnings are enabled.
  * `annotations`: The nullable annotation context is enabled. The nullable warning context is disabled.
    * Variables of a reference type, string for example, are non-nullable. All nullability warnings are disabled.
  * `disable`: The nullable annotation context is disabled. The nullable warning context is disabled.
    * Variables of a reference type are oblivious, just like earlier versions of C#. All nullability warnings are disabled.
* `Analyzer/Source Generator Packages` option to analyze/generate your code on compile.

[OpenSesame.Net.Compilers]: https://www.nuget.org/packages/OpenSesame.Net.Compilers
[OpenSesame.Net.Compilers.Toolset]: https://www.nuget.org/packages/OpenSesame.Net.Compilers.Toolset
[Microsoft.Net.Compilers]: https://www.nuget.org/packages/Microsoft.Net.Compilers
[Microsoft.Net.Compilers.Toolset]: https://www.nuget.org/packages/Microsoft.Net.Compilers.Toolset

### Feature plans

* Show package description.

### NOTE: Please do so at your own risk!

https://forum.unity.com/threads/unity-c-8-support.663757/page-2#post-5738029

> Unity doesn't support using your own C# compiler.
> While this is possible, it is not something I would recommend, as we've not tested customer C# compiler versions with Unity.

<br><br><br><br>

## Installation

### Requirement

* Unity 2018.3 or later

### via OpenUPM

This package is available on [OpenUPM](https://openupm.com).  
After installing [openupm-cli](https://github.com/openupm/openupm-cli), run the following command
```
openupm add com.coffee.csharp-compiler-settings
```

### via Package Manager

Find the `manifest.json` file in the `Packages` directory in your project and edit it to look like this:
```
{
  "dependencies": {
    "com.coffee.csharp-compiler-settings": "https://github.com/NaoUnderscore/UnityCSCSettings.git",
    ...
  },
}
```
To update the package, change suffix `#{version}` to the target version.

* e.g. `"com.coffee.csharp-compiler-settings": "https://github.com/NaoUnderscore/UnityCSCSettings.git#1.0.0",`

Or, use [UpmGitExtension](https://github.com/mob-sakai/UpmGitExtension) to install and update the package.

<br><br><br><br>

## Usage

### Configure C# compiler settings for the project

1. Open project setting window. (`Edit > Project Settings`)  
![](https://user-images.githubusercontent.com/12690315/92742741-e3d3da00-f3ba-11ea-8314-4cabd88c1b2c.png)
2. Select `C# Compiler` tab
3. Set `Compiler Type` to `Custom Package`, to use custom compiler package.  
![](https://user-images.githubusercontent.com/12690315/97001486-62ec2e80-1573-11eb-9003-d40eb8ed8904.png)
1. Input `Package Name`, `Package Version`, `Language Version` for compilation.
   * See [features](#features) section.
2. Press `Apply` button to save settings.
3. It will automatically request a recompilation.  
The selected nuget package will be used for compilation.
6. Enjoy!

The project setting asset for C# Compiler will be saved in `ProjectSettings/CSharpCompilerSettings.asset`.

<br><br>

### Configure C# compiler settings for `*.asmodef` file

1. Select a `*.asmodef` file
2. Turn on `Enable C# Compilier Settings` to configure.  
![](https://user-images.githubusercontent.com/12690315/97001169-e3f6f600-1572-11eb-8504-c528130c2234.png)
3. Set `Compiler Type` to `Custom Package`, to use custom compiler package.
4. Input `Package Name`, `Package Version`, `Language Version` and `Modify Symbols` for compilation.
   * See [features](#features) section.
5. Press `Apply` button to save settings.

**Reload:** Reload C# compiler settings dll for the assembly.  
**Publish as dll:** Publish the assembly as dll to the parent directory.

<br><br>

### For C# 8.0 features

[C# 8.0 features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8)  
[samples](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/tree/sandbox/Assets/C%23%208.0%20Features)

If you want to use the C# 8.0 features, set it up as follows:

* Package Name: **Microsoft.Net.Compilers**
* Package Version: **3.5.0** or later
* Language Version: **latest** or `CSharp_8`

Some features required external library.

* Async stream -> [UniTask v2](https://github.com/Cysharp/UniTask)
  * Install to project.
* Indices and ranges -> [IndexRange](https://www.nuget.org/packages/IndexRange/)
  * Download nuget package and extract it.
  * Import `lib/netstandard2.0/IndexRange.dll` to project.
* Stackalloc in nested expressions -> [System.Memory](https://www.nuget.org/packages/System.Memory/)
  * Download nuget package and extract it.
  * Import `lib/netstandard2.0/System.Memory.dll` to project.

**NOTE:** Default interface methods feature is not available. It requires `.Net Standard 2.1`.

<br><br>

### For C# 9.0 features

[C# 9.0 features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9)  
[samples](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/tree/sandbox/Assets/C%23%209.0%20Features)

If you want to use the C# 9.0 features, set it up as follows:

* Package Name: **Microsoft.Net.Compilers**
* Package Version: **3.9.0** or later
* Language Version: **latest** or `CSharp_9`

**NOTE:** Some features is not available. It requires `.Net 5`.

<br><br>

### For C# 10.0 features

[C# 10.0 features](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)  
[samples](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/tree/sandbox/Assets/C%23%2010.0%20Features)

If you want to use the C# 10.0 features, set it up as follows:

* Package Name: **Microsoft.Net.Compilers.Toolset**
* Package Version: **4.0.1** or later
* Language Version: **latest** or `CSharp_10`

**NOTE:** Some features is not available. It requires `.Net 6`.

<br><br><br><br>

## Development Notes

### How to update CSharpCompilerSettings

- See [installation](#installation) section to update package
- Click `Project Settings > C# Compoler > Reload` to reload all `CSharpCompilerSettings_*.dll` assemblies 


<br><br>

### For Unity 2021.1 or later

In Unity 2021.1 or later, the compile flow has changed significantly. (#11)
The source code must pass both the built-in compiler and the custom compiler.
Please use the `CUSTOM_COMPILE` directive as follows:

```csharp
using System;
using NUnit.Framework;

namespace IgnoreAccessibility
{
    public class IgnoreAccessibilityContent
    {
        private int privateNumber = 999;
    }

    public class IgnoreAccessibilityTest
    {
        [Test]
        public void GetPrivateField()
        {
#if CUSTOM_COMPILE
            Assert.AreEqual(new IgnoreAccessibilityContent().privateNumber, 999);
#else
            throw new NotImplementedException();
#endif
        }
    }
}
```

<br><br>

### How to include the asmdef in `Packages` directory

By default, asmdefs under `Packages` and `Assets/Standard Assets/` are **NOT** compiled with the custom compiler package.  
This is to prevent unintended compiler changes and to minimize the impact of compiler changes.  
Packages (and store assets) should essentially be compiled with the default compiler.

![image](https://user-images.githubusercontent.com/12690315/103772620-29549b00-506d-11eb-915f-5a8b8c351d40.png)

If you want to compile an embedded package or your custom package with a custom compiler, set `Packages/your_package_name/` instead of `!Packages/`.  
See [#12](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/issues/12) for details.

<br><br><br><br>

## Contributing

### Issues

Issues are very valuable to this project.

- Ideas are a valuable source of contributions others can make
- Problems show where this project is lacking
- With a question you show where contributors can improve the user experience

### Pull Requests

Pull requests are, a great way to get your ideas into this repository.  
See [CONTRIBUTING.md](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/blob/master/CONTRIBUTING.md) and [sandbox](https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/tree/sandbox) branch.

### Support

This is an open source project that I am developing in my spare time.  
If you like it, please support me.  
With your support, I can spend more time on development. :)

[![](https://user-images.githubusercontent.com/12690315/50731629-3b18b480-11ad-11e9-8fad-4b13f27969c1.png)](https://www.patreon.com/join/mob_sakai?)  
[![](https://user-images.githubusercontent.com/12690315/66942881-03686280-f085-11e9-9586-fc0b6011029f.png)](https://github.com/users/mob-sakai/sponsorship)

<br><br><br><br>

## License

* MIT

## Author

* ![](https://user-images.githubusercontent.com/12690315/96986908-434a0b80-155d-11eb-8275-85138ab90afa.png) [mob-sakai](https://github.com/mob-sakai) [![](https://img.shields.io/twitter/follow/mob_sakai.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=mob_sakai) ![GitHub followers](https://img.shields.io/github/followers/mob-sakai?style=social)

## See Also

* GitHub page : https://github.com/mob-sakai/CSharpCompilerSettingsForUnity
* Releases : https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/releases
* Issue tracker : https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/issues
* Change log : https://github.com/mob-sakai/CSharpCompilerSettingsForUnity/blob/master/CHANGELOG.md
* Test Space : https://mob-sakai.testspace.com/spaces/130862/current
