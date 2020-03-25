# PartyBot [![Build status](https://ci.appveyor.com/api/projects/status/bi3vs4lpl1c02i38?svg=true)](https://ci.appveyor.com/project/joelp53/party-bot) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/5c5f7aec386d495587b60546f9659d42)](https://www.codacy.com/app/joelp53/Party-Bot?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=joelp53/Party-Bot&amp;utm_campaign=Badge_Grade)

### A Simple Discord Music Bot Designed For Medium Sized Servers.

## Built With

* [DotNet Core (Version - 2.1)](https://dotnet.microsoft.com/download/dotnet-core/2.2) - Dotnet version.
* [Discord.Net (Version - 2.0.0-beta2-01039)](https://github.com/RogueException/Discord.Net) - The Discord Library used
* [Victoria (Version - 3.1.2)](https://github.com/Yucked/Victoria) - LavaLink Library.

## Want to use this as a template to build off?
**NOTE: This Requires At-Least C# Version 7.2**
1. First Make sure you have a fork of this repo. (Makes it easier for you to then commit your own changes to your GitHub)
2. Next you will need to clone your forked repo onto your P.C.
3. Once it's on your machine you will want to run the command: ``dotnet restore`` (Do this in your prefered Terminal)
**Note: Make sure you do this in the same directory that ``PartyBot.sln`` is stored.** It should look something like this when done.
```bash
foo@bar:~$ dotnet restore
  Restoring packages for C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\PartyBot.csproj...
  Installing Microsoft.NETCore.DotNetAppHost 2.0.0.
  Installing Microsoft.Extensions.DependencyInjection.Abstractions 2.0.0.
  Installing Microsoft.NETCore.DotNetHostResolver 2.0.0.
  Installing Microsoft.NETCore.DotNetHostPolicy 2.0.0.
  Installing NETStandard.Library 2.0.0.
  Installing Microsoft.NETCore.App 2.0.0.
  Generating MSBuild file C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\obj\PartyBot.csproj.nuget.g.props.
  Generating MSBuild file C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\obj\PartyBot.csproj.nuget.g.targets.
  Restore completed in 4.94 sec for C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\PartyBot.csproj.
foo@bar:~$
```
4. Just to be safe we'll also do ``dotnet build`` which should output like so:
```bash
foo@bar:~$ dotnet build
Microsoft (R) Build Engine version 15.9.20+g88f5fadfbe for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 74.91 ms for C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\PartyBot.csproj.
  PartyBot -> C:\Users\YOURNAME\source\repos\PartyBot\PartyBot\bin\Debug\netcoreapp2.0\PartyBot.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.41
```
5. You can now open the .sln file with Visual Studio or open the folder structure in your prefered IDE/Text Editor.

## Authors

* **Draxis (Me)** - *Initial work* - [Drax](https://github.com/joelp53/)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Yucked for Victoria.
