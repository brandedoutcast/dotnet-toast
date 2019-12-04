using System.Linq;
using System.Threading;
using Windows.UI.Notifications;

namespace Toast
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = args[0], msg = args[1];

            var ToastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var Nodes = ToastXml.GetElementsByTagName("text");
            Nodes.ElementAt(0).InnerText = title;
            Nodes.ElementAt(1).InnerText = msg;

            ToastNotificationManager.CreateToastNotifier("Toast").Show(new ToastNotification(ToastXml));
            Thread.Sleep(1000);
        }
    }
}
