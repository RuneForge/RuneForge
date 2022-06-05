# RuneForge

A fantasy real-time strategy game engine based on the MonoGame framework and written in the C# programming language.

## Dependencies

This project utilizes the following frameworks, libraries, and technologies:

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) for cross-platform managed code.
- [MonoGame](https://www.monogame.net/) for easy-to-use graphics, sound, and input APIs, game cycle management options, etc.
- [AutoMapper](https://docs.automapper.org/en/stable/index.html) for an object-to-object convention-based mapper.
- [GitVersion](https://gitversion.net/) for a tool filling the assebly & file version info using Git branches and tags.
- [TimelessTales](https://github.com/Wargus/Timeless-Tales) for graphics assets.

## Installation Guide

You should have .NET Core 3.1 SDK and Runtime on your machine (see the [installation guide](https://dotnet.microsoft.com/en-us/download) for Windows, Linux and Mac OS).

Installing dependencies (starting in the root folder of the repository):

    cd Source
    dotnet restore .\RuneForge.sln
    dotnet build .\RuneForge.sln

Running unit tests:

    dotnet test .\RuneForge.sln

Starting the game:

    cd RuneForge
    dotnet run .\RuneForge.csproj

## Configuration

Description of application-specific configuration keys found in `appsettings.json`:

|        Section        |          Key          |                             Description                              | Example Value |
| :-------------------: | :-------------------: | :------------------------------------------------------------------: | :-----------: |
| GraphicsConfiguration |    BackBufferWidth    |                      Width of the back buffer.                       |     1920      |
| GraphicsConfiguration |   BackBufferHeight    |                      Height of the back buffer.                      |     1080      |
| GraphicsConfiguration | UseHardwareFullScreen | Hardware fullscreen mode switch. Leads to display reboot if enabled. |     false     |
| GraphicsConfiguration |     UseFullScreen     |                       Fullscreen mode switch.                        |     true      |

## Projects Description

The solution contains several projects that can be divided into 4 sections.

Projects representing the game application:

- RuneForge.csproj  
  A game application containing the .NET Core host configuration logic. Performs initializing of the game.
- RuneForge.Core.csproj  
  A class library project containing the engine-level features like graphics user interface and input handling.
- RuneForge.Game.csproj  
  A class library project containing the game-level features like entities, systems, components and component factories.
- RuneForge.Data.csproj  
  A class library project containing the data-layer entities supporting the serialization mechanism.

MonoGame Content Pipeline extensions used by the MonoGame Content Builder:

- RuneForge.Content.Pipeline.Core.csproj  
  A MonoGame Content Pipeline extension library containing content types for the RuneForge.Core project: texture and animation atlases, graphics user interface controls and resources.
- RuneForge.Content.Pipeline.Game.csproj  
  A MonoGame Content Pipeline extension library containing content types for the RuneForge.Game project: maps, unit and building prototypes.

Console tools speeding up the development & testing process:

- Tools\RuneForge.Tools.MapConverter.csproj  
  A console application converting `.png` and `.bmp` images into `.xml` map content files.

Unit test projects:

- Tests\RuneForge.Core.Tests.csproj  
  An MSTest unit test project containing unit tests for the engine-level types.
- Tests\RuneForge.Data.Tests.csproj  
  An MSTest unit test project containing unit tests for the data-layer types.
