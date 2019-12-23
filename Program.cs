using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.UI.Notifications;

namespace Toast
{
    struct Params
    {
        internal string AppName;
        internal string Message;
        internal string Header;
        internal string Footer;
        internal string ImgPath;
    }

    class Program
    {
        internal const string APP_NAME = "toast";
        static void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This tool produces Windows notifications only");
                return;
            }

            try
            {
                Process(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: \"{ex.Message}\", please report this @ https://github.com/rohith/dotnet-toast");
            }
        }

        static void Process(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            if (args.Length == 1)
            {
                if (args[0].StartsWith("-") && args[0].Length == 2)
                {
                    ShowHelp();
                    return;
                }

                ShowToast(args[0]);
            }

            var FlagCount = args.Where(a => a.StartsWith("-") && a.Length == 2).Count();
            var Params = new Params();
            if (FlagCount > 0)
            {
                for (int i = 0; i < args.Length - 1; i = i + 2)
                    switch (args[i])
                    {
                        case "-h":
                            Params.Header = args[i + 1];
                            break;
                        case "-m":
                            Params.Message = args[i + 1];
                            break;
                        case "-i":
                            Params.ImgPath = args[i + 1];
                            break;
                        case "-f":
                            Params.Footer = args[i + 1];
                            break;
                        case "-n":
                            Params.AppName = args[i + 1];
                            break;
                    }
            }
            else
            {
                Params.Header = args[0];
                Params.Message = args[1];

                if (args.Length > 2)
                    Params.Footer = args[2];
            }

            ShowToast(Params.Message, Params.Header, Params.Footer, Params.ImgPath, Params.AppName);
        }

        static void ShowVersion()
        {
            var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

            Console.WriteLine($"{APP_NAME} v{versionString}");
            Console.WriteLine("----------------");
        }

        static void ShowHelp()
        {
            ShowVersion();

            Console.WriteLine($"\nUsage: {APP_NAME} [message]");
            Console.WriteLine($"       {APP_NAME} [header] [message] [footer]");
            Console.WriteLine($"       {APP_NAME} [options]");

            Console.WriteLine("\nNote: Every parameter except the message is optional");

            Console.WriteLine("\noptions:");
            Console.WriteLine("   -m : message to be displayed in toast");
            Console.WriteLine("   -h : header of the toast displayed on top in bold");
            Console.WriteLine("   -f : footer of the toast displayed at the bottom");
            Console.WriteLine("   -i : absolute file path of the image to be displayed in toast");
            Console.WriteLine("   -n : app name with which the toasts will be grouped together in Action Center, only visible in a toast when providing an image");

            Console.WriteLine("\nEx:");
            Console.WriteLine($"   {APP_NAME} \"Uncertainty and expectation are the joys of life. Security is an insipid thing\"");
            Console.WriteLine($"\n   {APP_NAME} \"Wikiquote\" \"Uncertainty and expectation are the joys of life. Security is an insipid thing\" \"~ William Congreve\"");
            Console.WriteLine($"\n   {APP_NAME} -m \"Uncertainty and expectation are the joys of life. Security is an insipid thing\" -h \"Wikiquote\" -f \"~ William Congreve\"");
            Console.WriteLine($"\n   {APP_NAME} -m \"Uncertainty and expectation are the joys of life. Security is an insipid thing\" -h \"Wikiquote\" -f \"~ William Congreve\" -i \"C:\\dotnet-toast\\icon.png\" -n \"dotnet-toast\"");

            Console.WriteLine();
        }

        internal static void ShowToast(string msg, string header = null, string footer = null, string imgSrc = null, string name = null)
        {
            Func<string, bool> empty = string.IsNullOrWhiteSpace;

            var ChosenToastType = (header, footer, imgSrc) switch
            {
                var (hdr, ftr, img) when empty(img) => (hdr, ftr) switch
                {
                    var (h, f) when !empty(h) && empty(f) => ToastTemplateType.ToastText02,
                    var (h, f) when !empty(h) && !empty(f) => ToastTemplateType.ToastText04,
                    var (h, f) when !empty(h) && empty(f) && h.Length > msg.Length => ToastTemplateType.ToastText03,
                    _ => ToastTemplateType.ToastText01
                },
                var (hdr, ftr, img) when !empty(img) => (hdr, ftr) switch
                {
                    var (h, f) when !empty(h) && empty(f) => ToastTemplateType.ToastImageAndText02,
                    var (h, f) when !empty(h) && !empty(f) => ToastTemplateType.ToastImageAndText04,
                    var (h, f) when !empty(h) && empty(f) && h.Length > msg.Length => ToastTemplateType.ToastImageAndText03,
                    _ => ToastTemplateType.ToastImageAndText01,
                },
                _ => ToastTemplateType.ToastText01
            };

            var ToastXml = ToastNotificationManager.GetTemplateContent(ChosenToastType);
            var Nodes = ToastXml.GetElementsByTagName("text");

            if (Nodes.Length == 1)
                Nodes.ElementAt(0).InnerText = msg;
            else if (Nodes.Length == 2 || Nodes.Length == 3)
            {
                Nodes.ElementAt(0).InnerText = header;
                Nodes.ElementAt(1).InnerText = msg;

                if (Nodes.Length == 3)
                    Nodes.ElementAt(2).InnerText = footer;
            }

            var ImgNodes = ToastXml.GetElementsByTagName("image");
            if (ImgNodes.Length > 0)
                ImgNodes[0].Attributes[1].NodeValue = imgSrc;

            ToastNotificationManager.CreateToastNotifier(name ?? APP_NAME).Show(new ToastNotification(ToastXml));
            Thread.Sleep(1000);
        }
    }
}
