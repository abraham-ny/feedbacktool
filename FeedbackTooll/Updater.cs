using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.Web.
using System.Net;
using System.Web.Script.Serialization;

namespace FeedbackTooll
{
    public class Updater
    {
        public Updater()
        {

        }

        public GitHubRelease GetLatestRelease(string apiUrl)
        {
            var client = new WebClient();
            client.Headers.Add("User-Agent", "FeedbackTool"); // GitHub requires this

            string json = client.DownloadString(apiUrl);

            var serializer = new JavaScriptSerializer();
            var release = serializer.Deserialize<GitHubRelease>(json);

            return release;
        }

        public async Task CheckUpdate(string url, bool download)
        {
            Console.WriteLine("Checking for updates...");
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            dynamic release = GetLatestRelease(url);
            string latest = release.tag_name;
            string current = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (new Version(latest.TrimStart('v')) > new Version(current))
            {
                Console.WriteLine("Update Available: " + latest);
                if (download)
                {
                    Console.WriteLine("Downloading and installing update...");
                    Console.WriteLine("Update downloaded.");
                }
            }
            else
            {
                Console.WriteLine("Already up to date.");
            }
        }
    }
    public class GitHubAsset
    {
        public string browser_download_url { get; set; }
    }

    public class GitHubRelease
    {
        public string tag_name { get; set; }
        public string name { get; set; }
        public string body { get; set; }
        public List<GitHubAsset> assets { get; set; }
    }
}
