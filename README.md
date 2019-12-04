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
        notification heading appearing in bold
    
    message:
        notification message that appears below title

    Ex:
        toast "Toaster" "It's time to send a message"