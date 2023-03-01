using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AgeCounting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://www.dropbox.com/s/ecfe8st296hztxf/age-counting.json?dl=1");
                res.EnsureSuccessStatusCode(); 

                string body = await res.Content.ReadAsStringAsync();
                JObject result = JObject.Parse(body);

                int count = 0;

                foreach (var data in result["data"].ToString().Split(", "))
                {
                    if (data.StartsWith("age="))
                    {
                        int age = int.Parse(data.Substring(4));
                        if (age >= 50)
                        {
                            count++;
                        }
                    }
                }

                Console.WriteLine("The items with age greater than 50 are: "+ count);
            }
        }
    }
}
