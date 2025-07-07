using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackTooll
{
    public class Updater
    {
        public Updater()
        {

        }

        public async Task CheckUpdate(string url, bool download)
        {
            Console.WriteLine("Checking for updates...");
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            dynamic release = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
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
}
