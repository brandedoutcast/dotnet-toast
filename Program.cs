using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.UI.Notifications;

namespace Toast
{
    class Program
    {
        const string APP_NAME = "toast";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                ShowHelp();
                return;
            }

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("This tool can only produce Windows 10 notifications for now");
                return;
            }

            ShowToast(args[0], args[1]);
        }

        static void ShowToast(string title, string msg)
        {
            var ToastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var Nodes = ToastXml.GetElementsByTagName("text");
            Nodes.ElementAt(0).InnerText = title;
            Nodes.ElementAt(1).InnerText = msg;

            ToastNotificationManager.CreateToastNotifier(APP_NAME).Show(new ToastNotification(ToastXml));
            Thread.Sleep(1000);
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

            Console.WriteLine($"\nUsage: {APP_NAME} [title] [message]");

            Console.WriteLine("\ntitle:");
            Console.WriteLine("   toast heading displayed as top line in bold");

            Console.WriteLine("\nmessage:");
            Console.WriteLine("   toast message shown below heading wrapped in 2 lines");

            Console.WriteLine("\nEx:");
            Console.WriteLine($"   {APP_NAME} \"Toaster\" \"It's time to send a message\"");
            Console.WriteLine();
        }
    }
}
