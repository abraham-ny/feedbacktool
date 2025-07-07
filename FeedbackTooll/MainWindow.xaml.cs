using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FeedbackTooll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string mode;
        private string url;
        public MainWindow(string[] args)
        {
            InitializeComponent();
            ParseArgs(args);

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void ParseArgs(string[] args)
        {
            if (args.Length > 1)
            {
                mode = args[1].ToLower();
                url = GetArgValue(args, "-url");

                if (mode == "-update")
                {
                    bool download = GetArgValue(args, "-downloadinstall")?.ToLower() == "true";
                    UpdateTab.IsEnabled = true;
                    MainTabs.SelectedItem = UpdateTab;
                    Updater upd = new Updater();
                    upd.CheckUpdate(url, download).Wait();
                }
                else if (mode == "-feedback")
                {
                    FeedbackTab.IsEnabled = true;
                    MainTabs.SelectedItem = FeedbackTab;
                    //string url = GetArgValue(args, "-url");
                    string message = GetArgValue(args, "-message");
                    string user = GetArgValue(args, "-username") ?? "anonymous";
                    Feedback fb = new Feedback();
                    fb.SendFeedback(url, message, user).Wait();
                }
                else
                {
                    MessageBox.Show("Invalid mode");
                    Application.Current.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("No arguments provided.");
                Application.Current.Shutdown();
            }
        }

        private string GetArgValue(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index >= 0 && index < args.Length - 1)
                return args[index + 1];
            return null;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12)
            {
                var dev = new Devtool(url);
                dev.Owner = this;
                dev.ShowDialog();
            }
        }
    }
}
