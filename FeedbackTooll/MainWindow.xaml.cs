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
        string[] margs;
        public MainWindow(string[] args)
        {
            InitializeComponent();
            if (args.Length<1)
            {
                ParseArgs(args);
                margs = args;
            }
            else if(args.Equals(null)){
                FeedbackTab.IsEnabled = true;
                messageTextBlock.Text = "null: No url was provided, please close the app and try again with the right parameters";
            }
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
            sendBtn.Click += SendBtn_Click;
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            handleFeedback(margs);
        }

        private void ParseArgs(string[] args)
        {
            if (args.Length > 1)
            {
                mode = args[1].ToLower();
                url = GetArgValue(args, "-url");
                margs = args;

                if (mode == "-update")
                {
                    bool download = GetArgValue(args, "-downloadinstall")?.ToLower() == "true";
                    UpdateTab.IsEnabled = true;
                    FeedbackTab.IsEnabled = false;
                    MainTabs.SelectedItem = UpdateTab;
                    Updater upd = new Updater();
                    //upd.CheckUpdate(url, download).Wait();

                    var release = new GitHubRelease
                    {
                        tag_name = "v1.0.0.0",
                        name = "null",
                        body = null,
                        assets = null
                    };
                    try { 
                        release = upd.GetLatestRelease(url);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Github Release error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Console.WriteLine("Latest Version: " + release.tag_name);
                    Console.WriteLine("Download URL: " + release.assets[0].browser_download_url);
                    messageTextBlock.Text += "Latest Version: " + release.tag_name + "\n" 
                        + "Download URL: " + release.assets[0].browser_download_url;

                    // Compare versions
                    string current = "1.0.0.0";
                    try
                    {
                        current = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, "Version getter error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    Version latest = new Version(release.tag_name.TrimStart('v'));

                    if (latest > new Version(current))
                    {
                        Console.WriteLine("Update available!");
                        messageTextBlock.Text += "\nIt's recommended to keep your software updated with the latest bug fixes, vulnerability patches and new features / improvements.";
                        downloadBtn.IsEnabled = true;
                    }
                    else if (latest == new Version(current))
                    {
                        Console.WriteLine("Already up to date.");
                        messageTextBlock.Text += "You're all good, come back later.";
                    }
                    else
                    {
                        Console.WriteLine("An error occurred");
                        messageTextBlock.Text += "Something's wrong with the network.";
                    }
                }
                else if (mode == "-feedback" && GetArgValue(args, "-message")!=null|| !GetArgValue(args, "-message").Equals(""))
                {
                    handleFeedback(args);
                    
                }
                else
                {
                    //MessageBox.Show("Invalid mode");
                    //Application.Current.Shutdown();
                }
            }
            else
            {
                MessageBox.Show("No arguments provided.");
                //Application.Current.Shutdown();
            }
        }

        void handleFeedback(string[] args)
        {
            FeedbackTab.IsEnabled = true;
            MainTabs.SelectedItem = FeedbackTab;
            Feedback fb = new Feedback();

            //string url = GetArgValue(args, "-url");
            string message = GetArgValue(args, "-message") ?? "feedbacktool message (usually means no user message)";
            string user = GetArgValue(args, "-username") ?? "anonymous";
            userNameBox.Text = user;
            if (message != null)
            {
                try
                {
                    fb.SendFeedback(url, message, user).Wait();
                }
                catch (Exception e)
                {
                    userFeedback.Text = e.Message;
                }
                userFeedback.Text = message;
            }
            else
            {
                userFeedback.Text = "Enter Message Here";
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
                dev.Show();//added this line to test wether the below lines caused the issue
                /*dev.Owner = this;
                dev.ShowDialog();*/
            }
        }
    }
}
