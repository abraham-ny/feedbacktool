using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackTooll
{
    public class Feedback
    {
        //public string url,message,username;
        public Feedback() {
        }
        public async Task SendFeedback(string url, string message, string username)
        {
            Console.WriteLine("Sending feedback...");
            var client = new HttpClient();
            var content = new StringContent(
                $"{{\"username\":\"{username}\",\"message\":\"{message}\"}}",
                Encoding.UTF8,
                "application/json"
            );

            var result = await client.PostAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Feedback sent successfully.");
                //return 0;
            }
            else
            {
                Console.WriteLine("Feedback failed: " + result.StatusCode);
            }
        }
    }
}
