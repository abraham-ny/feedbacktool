using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FeedbackTooll
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Equals(null))
            {
                new MainWindow(null).Show();
            }
            if (args.Contains("-nogui"))
            {
                HandleNoGuiMode(args);
                Shutdown(); // Prevent WPF window from showing
            }
            else
            {
                new MainWindow(args).Show();
            }
        }
        private void HandleNoGuiMode(string[] args)
        {
            Console.WriteLine("FeedbackTool started with -nogui mode");
            try
            {
                if (args.Contains("-update"))
                {
                    string url = GetArgValue(args, "-update");
                    bool download = GetArgValue(args, "-downloadinstall")?.ToLower() == "true";
                    Updater upd = new Updater();
                    upd.CheckUpdate(url, download).Wait();
                }
                else if (args.Contains("-feedback"))
                {
                    string url = GetArgValue(args, "-url");
                    string message = GetArgValue(args, "-message");
                    string user = GetArgValue(args, "-username") ?? "anonymous";
                    Feedback fb = new Feedback();
                    fb.SendFeedback(url, message, user).Wait();
                }
                else
                {
                    Console.WriteLine("Invalid -nogui mode. Must specify -update or -feedback.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("nogui error: " + ex.Message);
            }
        }

        private string GetArgValue(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index >= 0 && index < args.Length - 1)
                return args[index + 1];
            return null;
        }
    }
}
