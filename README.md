# ✨ toast [dotnet-toast]

.NET Core global CLI tool to to create toast notifications on Windows 8 / 8.1 / 10

Screenshot | Description
--- | ---
![text](https://raw.githubusercontent.com/rohith/dotnet-toast/master/screens/txt-all.jpg) | Message with a header & footer
![image](https://raw.githubusercontent.com/rohith/dotnet-toast/master/screens/img-all.jpg) | Message with image, header, footer & name

## Install

The `dotnet-toast` nuget package is [published to nuget.org](https://www.nuget.org/packages/dotnet-toast/)

You can get the tool by running this command

`$ dotnet tool install -g dotnet-toast`

## Usage

    toast -h

    Usage: toast [message]
           toast [header] [message] [footer]
           toast [options]

    Note: Every parameter except the message is optional

    options:
        -m: message to be displayed in toast
        -h: header of the toast displayed on top in bold
        -f: footer of the toast displayed at the bottom
        -i: absolute file path of the image to be displayed in toast
        -n: app name with which the toasts will be grouped together in Action Center, only visible in a toast when providing an image

    Ex:
        toast "Uncertainty and expectation are the joys of life. Security is an insipid thing"
        
        toast "Wikiquote" "Uncertainty and expectation are the joys of life. Security is an insipid thing"
        
        toast "Wikiquote" "Uncertainty and expectation are the joys of life. Security is an insipid thing" "~ William Congreve"
        
        toast -m "Uncertainty and expectation are the joys of life. Security is an insipid thing" -i "C:\dotnet-toast\icon.png"
        
        toast -h "Wikiquote" -m "Uncertainty and expectation are the joys of life. Security is an insipid thing" -i "C:\dotnet-toast\icon.png"
        
        toast -m "Uncertainty and expectation are the joys of life. Security is an insipid thing" -i "C:\dotnet-toast\icon.png" -n "Wikiquote"
