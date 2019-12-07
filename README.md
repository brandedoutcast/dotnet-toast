# ✨ toast [dotnet-toast]

.NET Core global CLI tool to to create toast notifications on Windows 10

## Install

The `dotnet-toast` nuget package is [published to nuget.org](https://www.nuget.org/packages/dotnet-toast/)

You can get the tool by running this command

`$ dotnet tool install -g dotnet-toast`

![screenshot](https://raw.githubusercontent.com/rohith/dotnet-toast/master/screenshot.jpg)

## Usage

    Usage: toast [title] [message]

    title:
        toast heading displayed as top line in bold
    
    message:
        toast message shown below heading wrapped in 2 lines

    Ex:
        toast "Toaster" "It's time to send a message"