# poy-speedrun-mod
![UILib](https://img.shields.io/badge/Made%20with-UILib-e24a8c?style=flat&link=https%3A%2F%2Fgithub.com%2FKaden5480%2Fpoy-ui-lib%2F
)
![Code size](https://img.shields.io/github/languages/code-size/Kaden5480/poy-speedrun-mod?color=5c85d6)
![Open issues](https://img.shields.io/github/issues/Kaden5480/poy-speedrun-mod?color=d65c5c)
![License](https://img.shields.io/github/license/Kaden5480/poy-speedrun-mod?color=a35cd6)

> [!IMPORTANT]
> This mod is incomplete and won't have an available release yet.
> If you know what you're doing and want to try it out, you can follow the [build instructions](#building-from-source).

A
[Peaks of Yore](https://store.steampowered.com/app/2236070/)
mod for speedrunning (made with
[UILib](https://github.com/Kaden5480/poy-ui-lib/)).

# Overview
- [Installing](#installing)
- [Building from source](#building-from-source)
    - [Dotnet](#dotnet-build)
    - [Visual Studio](#visual-studio-build)
    - [Build configuration](#build-configuration)

# Installing
### BepInEx
If you haven't installed BepInEx yet, follow the install instructions here:
- [Windows](https://github.com/Kaden5480/modloader-instructions#bepinex-windows)
- [Linux](https://github.com/Kaden5480/modloader-instructions#bepinex-linux)

### UILib
If you haven't installed UILib yet, follow the install instructions here:
- [UILib](https://github.com/Kaden5480/poy-ui-lib/?tab=readme-ov-file#installing)

### Speedrun Mod
- Download the latest release
[here](https://github.com/Kaden5480/poy-speedrun-mod/releases).
- The compressed zip will contain a `plugins` directory.
- Copy the files in `plugins` to `BepInEx/plugins` in your game directory.

# Building from source
Whichever approach you use for building from source, the resulting
plugin/mod can be found in `bin/`.

The following configurations are supported:
- Debug
- Release

## Dotnet build
To build with dotnet, run the following command, replacing
<configuration> with the desired value:
```sh
dotnet build -c <configuration>
```

## Visual Studio build
To build with Visual Studio, open `SpeedrunMod.sln` and build by pressing ctrl + shift + b,
or by selecting Build -> Build Solution.

## Build configuration
The following can be configured:
- The path Peaks of Yore is installed at.
- Whether the mod should automatically install on build.

Note that both of these properties are optional.

The configuration file must be in the root of this repository and must be called `Config.props`.
```xml
<Project>
  <PropertyGroup>
    <!-- For example, if peaks is installed under F: -->
    <GamePath>F:\Games\Peaks of Yore</GamePath>

    <!-- Add this option if you want to install after building -->
    <InstallAfterBuild>true</InstallAfterBuild>
  </PropertyGroup>
</Project>
```
